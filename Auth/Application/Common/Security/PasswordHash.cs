using System.Security.Cryptography;

namespace Application.Common.Security
{
	public class PasswordHash
	{
		// The following constants may be changed without breaking existing hashes.
		public const int SALT_BYTE_SIZE = 64;
		public const int HASH_BYTE_SIZE = 64;
		public const int PBKDF2_ITERATIONS = 1024;

		public const int ITERATION_INDEX = 0;
		public const int SALT_INDEX = 1;
		public const int PBKDF2_INDEX = 2;

		/// <summary>
		/// Creates a salted PBKDF2 hash of the password.
		/// </summary>
		/// <param name="password">The password to hash.</param>
		/// <returns>The hash of the password.</returns>
		public static byte[] CreateHash(string password, out string salt)
		{
			// Generate a random salt
			using var csprng = RandomNumberGenerator.Create();
			byte[] saltBytes = new byte[SALT_BYTE_SIZE];
			csprng.GetBytes(saltBytes);

			// Hash the password and encode the parameters
			byte[] hash = PBKDF2(password, saltBytes, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
			salt = Convert.ToBase64String(saltBytes);
			return hash;
		}

		/// <summary>
		/// Validates a password given a hash of the correct one.
		/// </summary>
		/// <param name="password">The password to check.</param>
		/// <param name="correctHash">A hash of the correct password.</param>
		/// <returns>True if the password is correct. False otherwise.</returns>
		public static bool ValidatePassword(string password, byte[] hash, string salt)
		{
			if (salt == null || hash == null || password == null)
				return false;

			// Extract the parameters from the hash
			byte[] saltBytes = Convert.FromBase64String(salt);

			byte[] testHash = PBKDF2(password, saltBytes, PBKDF2_ITERATIONS, hash.Length);
			return SlowEquals(hash, testHash);
		}

		/// <summary>
		/// Compares two byte arrays in length-constant time. This comparison
		/// method is used so that password hashes cannot be extracted from
		/// on-line systems using a timing attack and then attacked off-line.
		/// </summary>
		/// <param name="a">The first byte array.</param>
		/// <param name="b">The second byte array.</param>
		/// <returns>True if both byte arrays are equal. False otherwise.</returns>
		private static bool SlowEquals(byte[] a, byte[] b)
		{
			uint diff = (uint)a.Length ^ (uint)b.Length;
			for (int i = 0; i < a.Length && i < b.Length; i++)
				diff |= (uint)(a[i] ^ b[i]);
			return diff == 0;
		}

		/// <summary>
		/// Computes the PBKDF2-SHA512 hash of a password.
		/// </summary>
		/// <param name="password">The password to hash.</param>
		/// <param name="salt">The salt.</param>
		/// <param name="iterations">The PBKDF2 iteration count.</param>
		/// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
		/// <returns>A hash of the password.</returns>
		private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
		{
			using Rfc2898DeriveBytes pbkdf2 = new(password, salt, iterations, HashAlgorithmName.SHA512);
			return pbkdf2.GetBytes(outputBytes);
		}
	}
}
