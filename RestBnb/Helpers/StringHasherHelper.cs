using System.Security.Cryptography;
using System.Text;

namespace RestBnb.API.Helpers
{
    public static class StringHasherHelper
    {
        /// <summary>
        /// Generates hash from string using HMAC algorithm and returns a tuple containing values of created hash and salt
        /// </summary>
        /// <param name="stringToHash"></param>
        public static (byte[] hash, byte[] salt) HashStringWithHMACAndSalt(string stringToHash)
        {
            using var hmac = new HMACSHA512();

            return (hash: hmac.Key, salt: hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToHash)));
        }

        /// <summary>
        /// Checks if string and hash with salt given as arguments match
        /// </summary>
        /// <param name="stringToCheck"></param>
        /// <param name="hash"></param>
        /// <param name="salt"></param>
        public static bool DoesGivenStringMatchHashedString(string stringToCheck, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToCheck));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}