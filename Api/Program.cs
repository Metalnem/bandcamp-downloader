using Microsoft.Extensions.Logging;

namespace Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var username = Environment.GetEnvironmentVariable("BANDCAMP_USERNAME");
        var password = Environment.GetEnvironmentVariable("BANDCAMP_PASSWORD");

        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        using var client = new Client(username, password, loggerFactory);

        await foreach (var item in client.GetCollection())
        {
            Console.WriteLine($"{item.BandInfo.Name} - {item.Title}");
        }
    }
}
