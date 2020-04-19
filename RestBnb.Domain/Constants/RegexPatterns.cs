namespace RestBnb.Core.Constants
{
    public static class RegexPatterns
    {
        public static class User
        {
            public const string Password = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\\$%\\^&\\*])(?=.{8,})";
            public const string PhoneNumber = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{3}";
        }
    }
}
