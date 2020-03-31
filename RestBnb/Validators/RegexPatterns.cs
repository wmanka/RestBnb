namespace RestBnb.API.Validators
{
    public static class RegexPatterns
    {
        public static class User
        {
            public const string Password = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\\$%\\^&\\*])(?=.{8,})";
        }
    }
}
