namespace RestBnb.Core.Services
{
    public interface IStringHasherService
    {
        public (byte[] hash, byte[] salt) HashStringWithHmacAndSalt(string stringToHash);
        public bool DoesGivenStringMatchHashedString(string stringToCheck, byte[] hash, byte[] salt);
    }
}
