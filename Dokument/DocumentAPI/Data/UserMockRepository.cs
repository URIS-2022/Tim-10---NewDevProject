using DocumentAPI.Entities;
using System.Security.Cryptography;

namespace DocumentAPI.Data
{
	public class UserMockRepository : IUserRepository
	{
		public static List<User> Users { get; set; } = new List<User>();
		private readonly static int iterations = 1000;

		public UserMockRepository()
		{
			FillData();
		}

		/// <summary>
		/// Writes test data
		/// </summary>
		private void FillData()
		{
			var user1 = HashPassword("testpassword");

			Users.AddRange(new List<User>
			{
				new User
				{
					id = Guid.Parse("CFD7FA84-8A27-4119-B6DB-5CFC1B0C94E1"),
					firstName = "Sandra",
					lastName = "Melovic",
					userName = "sandra.melovic",
					email = "sandra.melovic@testmail.com",
					password = user1.Item1,
					salt = user1.Item2
				}
			});
		}

		/// <summary>
		/// Checking if there is a user with the passed credentials
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public bool UserWithCredentialsExists(string username, string password)
		{
			//If the username is unique, this is ok
			User user = Users.FirstOrDefault(u => u.userName == username);

			if (user == null)
			{
				return false;
			}

			//If there is a user with that username then the password is checked
			if (VerifyPassword(password, user.password, user.salt))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Hashing of password
		/// </summary>
		/// <param name="password">Password</param>
		/// <returns>Generated hash and salt</returns>
		private static Tuple<string, string> HashPassword(string password)
		{
			var sBytes = new byte[password.Length];
			new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);
			var salt = Convert.ToBase64String(sBytes);

			var derivedBytes = new Rfc2898DeriveBytes(password, sBytes, iterations);

			return new Tuple<string, string>
			(
				Convert.ToBase64String(derivedBytes.GetBytes(256)),
				salt
			);
		}

		/// <summary>
		/// Validates the passed password with the passed hash
		/// </summary>
		/// <param name="password">Password</param>
		/// <param name="savedHash">Saved hash</param>
		/// <param name="savedSalt">Saved salt</param>
		/// <returns></returns>
		public bool VerifyPassword(string password, string savedHash, string savedSalt)
		{
			var saltBytes = Convert.FromBase64String(savedSalt);
			var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
			if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedHash)
			{
				return true;
			}
			return false;
		}

	}
}
