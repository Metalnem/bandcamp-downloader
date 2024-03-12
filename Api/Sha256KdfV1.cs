namespace Api;

public static class Sha256KdfV1
{
    private const string Key =
        "1YlODulAdUFx6qKC3PzI5QP9gGuhPdIG" +
        "xCQJz25vTiwiDRbhZRdmz89NGmNOy5FM" +
        "vqcGAbRi3vrBD6vbwQriX2PSs9iRfWpc" +
        "G4kn9mlPbwQRjWVAtiwb5PMKNTcuuY92";

    public static string DeriveKey(string dm)
    {
        string key = Key;
        int iterations = (dm.LastCharToInt() * 3) % 4;

        for (int i = 0; i < iterations; ++i)
        {
            int mid = (int)Math.Floor(key.Length / 2.0);

            string s1 = key.Substring(0, mid).Reverse();
            string s2 = key.Substring(mid).Reverse().ToUpper();

            key = Crypto.HmacSha256(s1 + s2, key);
        }

        return key;
    }
}
