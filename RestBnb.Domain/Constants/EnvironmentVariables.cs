using System;

namespace RestBnb.Core.Constants
{
    public static class EnvironmentVariables
    {
        public static readonly string SendGridApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
    }
}
