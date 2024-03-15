using System.CommandLine;
using Microsoft.Extensions.Logging.Abstractions;

namespace Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var username = CreateOption("--username", "Your Bandcamp account username.");
        var password = CreateOption("--password", "Your Bandcamp account password.");
        var album = CreateOption("--album", "ID of the album you want to download.");

        var listCommand = new RootCommand("List all albums in your Bandcamp collection.")
        {
            username,
            password,
        };

        var downloadCommand = new Command("download", "Download the album with the specified ID.")
        {
            username,
            password,
            album
        };

        listCommand.AddCommand(downloadCommand);
        listCommand.SetHandler(List, username, password);
        downloadCommand.SetHandler(Download, username, password, album);

        await listCommand.InvokeAsync(args);
    }

    private static async Task List(string username, string password)
    {
        using var client = new Client(username, password, NullLoggerFactory.Instance);

        await foreach (var item in client.GetCollection())
        {
            var id = item.TralbumId;
            var artist = item.Artist ?? item.BandInfo.Name;
            var album = item.Title;

            Console.WriteLine($"{id,10} {artist} — {album}");
        }
    }

    private static void Download(string username, string password, string album)
    {
        throw new NotImplementedException();
    }

    private static Option<string> CreateOption(string name, string description)
    {
        return new Option<string>(name, description)
        {
            IsRequired = true
        };
    }
}
