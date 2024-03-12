using System.Security.Cryptography;
using System.Text;
using HashFunc = System.Func<byte[], byte[], byte[]>;

namespace Api;

public static class Crypto
{
    public static string HmacSha256(string key, string source)
    {
        return HashData(HMACSHA256.HashData, key, source);
    }

    public static string HmacSha1(string key, string source)
    {
        return HashData(HMACSHA1.HashData, key, source);
    }

    private static string HashData(HashFunc hashFunc, string key, string source)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var sourceBytes = Encoding.UTF8.GetBytes(source);
        var hash = hashFunc(keyBytes, sourceBytes);

        return Convert.ToHexString(hash).ToLower();
    }
}
