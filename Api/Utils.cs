namespace Api;

public static class Utils
{
    private static readonly char[] _invalidFileNameChars =
        new char[] { '\"', '<', '>', '|', ':', '*', '?', '\\', '/' };

    private const string Base36Chars = "0123456789abcdefghijklmnopqrstuvwxyz";

    public static string Reverse(this string s)
    {
        return new string(Enumerable.Reverse(s).ToArray());
    }

    public static int CharToInt(this string s, int index)
    {
        return Convert.ToInt32(s.Substring(index, 1), 16);
    }

    public static int LastCharToInt(this string s)
    {
        return s.CharToInt(s.Length - 1);
    }

    public static string ToBase36(int value)
    {
        string result = string.Empty;

        while (value > 0)
        {
            result = Base36Chars[value % 36] + result;
            value /= 36;
        }

        return result;
    }

    public static string GetSafeFileName(string trackTitle)
    {
        var fileName = trackTitle
            .Replace("?", string.Empty)
            .Replace('\"', '\'');

        foreach (var invalidChar in _invalidFileNameChars)
        {
            fileName = fileName.Replace(invalidChar, '-');
        }

        return fileName.Trim();
    }
}
