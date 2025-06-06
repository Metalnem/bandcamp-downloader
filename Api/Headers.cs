using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Api;

public static class Headers
{
	private const int AlgorithmHmacSha256 = 3;
	private const int AlgorithmHmacSha512 = 4;

	public static string CalculateDm(string dm, string requestBody)
	{
		int algorithm = 0;

		if (dm != null)
		{
			int index = dm.LastCharToInt();
			algorithm = dm.CharToInt(index);
		}

		if (algorithm == AlgorithmHmacSha256)
		{
			var key = Sha256KdfV1.DeriveKey(dm);
			var prefix = Sha256KdfV2.DeriveKey(dm);

			return Crypto.HmacSha256(key, prefix + requestBody);
		}

		if (algorithm == AlgorithmHmacSha512)
		{
			throw new Exception("HMAC SHA-516 KDF is currently not supported.");
		}

		return Crypto.HmacSha1("dtmfa", Sha1Kdf.DeriveKey(algorithm, dm) + requestBody);
	}

	public static string CalculatePow(string pow, string requestBody)
	{
		var parts = pow.Split(":");
		var data = requestBody + parts[2];
		var leadingZeroBits = int.Parse(parts[1]);
		var nonce = CalculateNonce(data, leadingZeroBits);

		return $"{parts[0]}:{parts[1]}:{parts[2]}:{nonce}";
	}

	private static string CalculateNonce(string data, int leadingZeroBits)
	{
		for (int nonce = 0; ; ++nonce)
		{
			var result = Utils.ToBase36(nonce);
			var bytes = Encoding.UTF8.GetBytes(data + result);
			var digest = SHA1.HashData(bytes);
			var total = 0;

			for (int i = 0; i < digest.Length; ++i)
			{
				int count = BitOperations.LeadingZeroCount(digest[i]) - 24;
				total += count;

				if (count < 8)
				{
					break;
				}
			}

			if (total >= leadingZeroBits)
			{
				return result;
			}
		}
	}
}
