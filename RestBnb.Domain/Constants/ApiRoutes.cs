namespace RestBnb.Core.Constants
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Auth
        {
            public const string Login = Base + "/auth/login";
            public const string Register = Base + "/auth/register";
            public const string Refresh = Base + "/auth/refresh";
        }

        public static class Properties
        {
            public const string GetAll = Base + "/properties";
            public const string Create = Base + "/properties";
            public const string Get = Base + "/properties/{propertyId}";
            public const string Put = Base + "/properties/{propertyId}";
            public const string Patch = Base + "/properties/{propertyId}";
            public const string Delete = Base + "/properties/{propertyId}";
        }

        public static class Bookings
        {
            public const string GetAll = Base + "/bookings";
            public const string Create = Base + "/bookings";
            public const string Get = Base + "/bookings/{bookingId}";
            public const string Put = Base + "/bookings/{bookingId}";
            public const string Patch = Base + "/bookings/{bookingId}";
            public const string Delete = Base + "/bookings/{bookingId}";
        }

        public static class HealthChecks
        {
            public const string Details = "/health";
        }
    }
}