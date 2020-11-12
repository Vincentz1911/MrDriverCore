using System;
using System.Security.Cryptography;

namespace MrDriverCore.Data
{
    public class CryptographyService
    {
        private const int SaltSize = 8;
        private const int HashSize = 16;
        private const int Iterations = 10000;

        public static string Hash(string password)
        {
            // Create salt
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] salt;
                rng.GetBytes(salt = new byte[SaltSize]);
                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
                {
                    byte[] hash = pbkdf2.GetBytes(HashSize);
                    // Combine salt and hash
                    byte[] hashBytes = new byte[SaltSize + HashSize];
                    Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
                    // Convert to base64
                    string base64Hash = Convert.ToBase64String(hashBytes);

                    // Format hash with extra information
                    return $"$HASH|V1${Iterations}${base64Hash}";
                }
            }
        }

        public static bool Verify(string password, string hashedPassword)
        {
            // Check hash
            if (!hashedPassword.Contains("HASH|V1$"))
                throw new NotSupportedException("The hashtype is not supported");

            // Extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace("$HASH|V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Create hash with given salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // Get result
                for (var i = 0; i < HashSize; i++) 
                    if (hashBytes[i + SaltSize] != hash[i]) 
                        return false;
                return true;
            }
        }
    }
}
