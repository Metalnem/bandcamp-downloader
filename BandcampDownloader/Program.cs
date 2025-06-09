using System.CommandLine;
using System.IO.Compression;
using Api;
using Api.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace BandcampDownloader;

public class Program
{
	// LoggerFactory.Create(builder => builder.AddConsole())
	private static readonly ILoggerFactory logger = NullLoggerFactory.Instance;

	public static async Task Main(string[] args)
	{
		var username = CreateOption<string>("--username", "Your Bandcamp account username.");
		var password = CreateOption<string>("--password", "Your Bandcamp account password.");
		var albumId = CreateOption<long>("--album", "ID of the album you want to download.");

		var disabledOnly = CreateOption<bool>(
			"--disabled-only",
			"List only albums that have been removed from your collection.",
			isRequired: false
		);

		var listCommand = new RootCommand("List all albums in your Bandcamp collection.")
		{
			username,
			password,
			disabledOnly
		};

		var downloadCommand = new Command("download", "Download the album with the specified ID.")
		{
			username,
			password,
			albumId
		};

		listCommand.AddCommand(downloadCommand);
		listCommand.SetHandler(List, username, password, disabledOnly);
		downloadCommand.SetHandler(Download, username, password, albumId);

		await listCommand.InvokeAsync(args);
	}

	private static async Task List(string username, string password, bool disabledOnly)
	{
		using var client = new Client(username, password, logger);

		await foreach (var item in client.GetCollection())
		{
			if (disabledOnly)
			{
				var details = await client.GetAlbumDetails(item.TralbumId);

				if (details?.BandcampUrl?.Contains("-disabled-") == true)
				{
					PrintAlbumDetails(item);
				}

				await Task.Delay(TimeSpan.FromSeconds(1));
			}
			else
			{
				PrintAlbumDetails(item);
			}
		}
	}

	private static void PrintAlbumDetails(Album album)
	{
		var id = album.TralbumId;
		var artist = album.Artist ?? album.BandInfo.Name;
		var title = album.Title;

		Console.WriteLine($"{id,10} {artist} — {title}");
	}

	private static async Task Download(string username, string password, long albumId)
	{
		using var client = new Client(username, password, logger);

		var album = await client.GetAlbum(albumId);

		if (album == null)
		{
			throw new Exception($"Album {albumId} is not available.");
		}

		if (album.Tracks == null)
		{
			throw new Exception($"{album.Title} doesn't have any tracks.");
		}

		using var output = new MemoryStream();
		using var httpClient = new HttpClient();

		using (var archive = new ZipArchive(output, ZipArchiveMode.Create, true))
		{
			foreach (var track in album.Tracks)
			{
				var fileName = $"{(track.TrackNumber ?? 1):00}. {Utils.GetSafeFileName(track.Title)}.mp3";
				var entry = archive.CreateEntry(fileName);

				using var entryStream = entry.Open();
				using var audioStream = await httpClient.GetStreamAsync(track.HqAudioUrl);

				await audioStream.CopyToAsync(entryStream);
			}

			try
			{
				var entry = archive.CreateEntry("cover.jpg");
				var url = $"https://f4.bcbits.com/img/a{album.ArtId}_1.jpg";

				using var stream = await httpClient.GetStreamAsync(url);
				using var entryStream = entry.Open();

				await stream.CopyToAsync(entryStream);
			}
			catch
			{
				// Failure to download the album art shouldn't abort the program
			}
		}

		var artist = album.Artist ?? album.BandInfo.Name;
		var archiveName = $"{artist} - {album.Title}";

		using var file = File.Create(
			path: $"{Utils.GetSafeFileName(archiveName)}.zip",
			bufferSize: 4096,
			options: FileOptions.Asynchronous
		);

		output.Position = 0;
		await output.CopyToAsync(file);
	}

	private static Option<T> CreateOption<T>(string name, string description, bool isRequired = true)
	{
		return new Option<T>(name, description)
		{
			IsRequired = isRequired
		};
	}
}
