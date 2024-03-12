namespace Api;

public static class Sha256KdfV2
{
    private const string Key =
        "YvtxhlQJoACdthoZcZaFlxmc62BN2HC5" +
        "7hwG64vSd90EPXfIetlovBmh9cfHkUN7" +
        "gvNj6iEbqQReseKxFcrjaESLZDxQbva4" +
        "jIBht7R27dmrussXayASiJ0YPnXZIJZs";

    public static string DeriveKey(string dm)
    {
        string key = Key;
        int iterations = dm.LastCharToInt();

        for (int i = 0; i < iterations; ++i)
        {
            int mid = (int)Math.Floor(key.Length / 2.0);

            string s1 = key.Substring(0, mid).Reverse();
            string s2 = key.Substring(mid).Reverse();

            key = Crypto.HmacSha256(s1 + s2, key);
        }

        return Crypto.HmacSha256(key, dm);
    }
}
