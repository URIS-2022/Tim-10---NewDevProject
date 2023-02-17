﻿using Contract.Entities;
using System.Security.Cryptography;

namespace Contract.Data
{
    public class UserMock : IUserRepository
    {
        public static List<User> Users { get; set; } = new List<User>();
        private readonly static int iterations = 1000;

        public UserMock()
        {
            FillData();
        }

        /// <summary>
        /// Test data method
        /// </summary>

        private void FillData()
        {
            var user1 = HashPassword("testpassword");

            Users.AddRange(new List<User>
            {
                new User
                {
                    id = Guid.Parse("2BCAD6E1-6696-4D88-AE26-221518B3A92D"),
                    firstName = "Petar",
                    lastName = "Petrovic",
                    userName = "petar.petrovic",
                    email = "petar.petrovic@testmail.com",
                    password = user1.Item1,
                    salt = user1.Item2
                }
            });
        }

        /// <summary>
        /// Checking if there is a person with the input credentials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserWithCredentialsExists(string username, string password)
        {
            User user = Users.FirstOrDefault(u => u.userName == username);

            if (user == null)
            {
                return false;
            }

            if (VerifyPassword(password, user.password, user.salt))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Hashing the password
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>Hash and salt</returns>
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
        /// Checking the passoword and hash
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="savedHash">Hash</param>
        /// <param name="savedSalt">Salt</param>
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
