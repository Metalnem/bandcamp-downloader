using Newtonsoft.Json;
using NUnit.Framework;

namespace Api.Tests;

public class HeadersTest
{
    private const string Username = "tidy.oil6575@fastmail.com";
    private const string Password = "MhdcjWP64aeNfTZH";

    [TestCase("da6b74b374809c6d7ad76c4c35cdd3f5056589ac7", "ae3ac5c1a6b371ad27b05d282813e757f8caecf792957d8654b49ca49506ff2e")]
    [TestCase("e0071819613bd08a28428b9c8903fc75dc0bd23da", "92b1a43d30d0fc3c8dee8f1ae10488f515c98aefed5d42c743d9d858dfc829d3")]
    public void TestDm(string incomingDm, string outgoingDm)
    {
        var parameters = new { email = Username, password = Password };
        var requestBody = JsonConvert.SerializeObject(parameters);
        var dm = Headers.CalculateDm(incomingDm, requestBody);

        Assert.That(dm, Is.EqualTo(outgoingDm));
    }

    [TestCase("1:10:c575eb0df40cc0a71bc45cce099bcf4638f5d1c2", "1:10:c575eb0df40cc0a71bc45cce099bcf4638f5d1c2:4d")]
    [TestCase("1:10:492827007f293a5884933ba5f62cbacdb60bb43d", "1:10:492827007f293a5884933ba5f62cbacdb60bb43d:2q4")]
    public void TestPow(string incomingPow, string outgoingPow)
    {
        var parameters = new { email = Username, password = Password };
        var requestBody = JsonConvert.SerializeObject(parameters);
        var pow = Headers.CalculatePow(incomingPow, requestBody);

        Assert.That(pow, Is.EqualTo(outgoingPow));
    }
}
