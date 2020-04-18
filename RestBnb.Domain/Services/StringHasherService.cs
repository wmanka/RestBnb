using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestBnb.Core.Services
{
    public class StringHasherService : IStringHasherService
    {
        public (byte[] hash, byte[] salt) HashStringWithHmacAndSalt(string stringToHash)
        {
            using var hmac = new HMACSHA512();
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));

            return (hash: computedHash, salt: hmac.Key);
        }

        public bool DoesGivenStringMatchHashedString(string stringToCheck, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToCheck));

            return !computedHash.Where((t, i) => t != hash[i]).Any();
        }
    }
}