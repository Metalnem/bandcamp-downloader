namespace Api;

public static class Sha1Kdf
{
	public static string DeriveKey(int algorithm, string dm)
	{
		if (dm == null)
		{
			return string.Empty;
		}

		if (algorithm == 1)
		{
			return dm.Substring(0, 19) + dm.Substring(22);
		}

		string key = dm.ToUpper();
		int floor = (int)Math.Floor(key.Length / 2.0);

		key = key.Substring(0, floor).Reverse() + key.Substring(floor).Reverse();
		key = key.Substring(0, 15) + key.Substring(19);
		key = key.Replace("4", "1");
		key = Crypto.HmacSha1("bntpd", key);

		return key.Replace("3", "5");
	}
}
