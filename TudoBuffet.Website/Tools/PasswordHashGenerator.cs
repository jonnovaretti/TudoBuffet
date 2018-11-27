using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace TudoBuffet.Website.Tools
{
    public class PasswordHashGenerator
    {
        public static string GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public static string CreateHashedTextFromText(string text, string salt)
        {
            string hashedText;
            byte[] saltBytes;

            saltBytes = Convert.FromBase64String(salt);

            hashedText = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                 password: text,
                                                 salt: saltBytes,
                                                 prf: KeyDerivationPrf.HMACSHA1,
                                                 iterationCount: 10000,
                                                 numBytesRequested: 256 / 8));

            return hashedText;
        }
    }
}
