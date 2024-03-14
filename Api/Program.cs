using Microsoft.Extensions.Logging.Abstractions;

namespace Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var username = Environment.GetEnvironmentVariable("BANDCAMP_USERNAME");
        var password = Environment.GetEnvironmentVariable("BANDCAMP_PASSWORD");

        using var client = new Client(username, password, NullLoggerFactory.Instance);

        await foreach (var item in client.GetCollection())
        {
            var id = item.TralbumId;
            var artist = item.Artist ?? item.BandInfo.Name;
            var album = item.Title;

            Console.WriteLine($"{id,10} {artist} — {album}");
        }
    }
}
