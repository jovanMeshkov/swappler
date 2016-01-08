using System;
using System.Security.Cryptography;

namespace Swappler.Utilities
{
    public static class HashHelper
    {
        // Default salt size for password. 128bit / 8bit = 16
        private static readonly int SaltSize = 128 / 8;

        // Default hashed size for password. 256bit / 8bit = 32
        private static readonly int HashedPasswordSize = 256 / 8;

        // Default iterations for password hashing.
        private static readonly int AlgorithmIterations = 1452;

        private static readonly char HashSaltSeparator = '#';

        public static string HashPassword(string password)
        {
            string salt = GenerateSalt(SaltSize);

            string hashedPassword = HashPassword(password, salt);

            return hashedPassword+HashSaltSeparator+salt;
        }
        
        public static bool VerifyPassword(string password, string hashedPasswordVerifier)
        {
            string[] hashedPasswordToken = hashedPasswordVerifier.Split(HashSaltSeparator);
            string hashedPassword = hashedPasswordToken[0];
            string hashedPasswordSalt = hashedPasswordToken[1];

            string expectedPasswordHash = HashPassword(password, hashedPasswordSalt);

            bool hashesEqual = true;
            for (int i = 0; i < expectedPasswordHash.Length; i++)
            {
                if (expectedPasswordHash[i] != hashedPassword[i])
                {
                    hashesEqual = false;
                }
            }
            return hashesEqual;
        }

        private static string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, AlgorithmIterations);

            byte[] hashedPasswordBytes = rfc2898DeriveBytes.GetBytes(HashedPasswordSize);

            string hashedPassword = Convert.ToBase64String(hashedPasswordBytes);

            return hashedPassword;
        }

        private static string GenerateSalt(int saltSize)
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();

            byte[] salt = new byte[saltSize];
            rngCryptoServiceProvider.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }
    }
}