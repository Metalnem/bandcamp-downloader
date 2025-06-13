using NUnit.Framework;

namespace Api.Tests;

public class ConsoleOutputTest
{
	[Test]
	public void TestOutput()
	{
		var expected = File.ReadAllLines("Expected.txt");
		var actual = File.ReadAllLines("Actual.txt");

		var expectedIds = new HashSet<string>(expected.Select(GetAlbumId));
		var actualIds = new HashSet<string>(actual.Select(GetAlbumId));

		expectedIds.ExceptWith(actualIds);
		Assert.That(expectedIds, Is.Empty);
	}

	private string GetAlbumId(string line)
	{
		var options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
		return line.Split(' ', options).FirstOrDefault();
	}
}
