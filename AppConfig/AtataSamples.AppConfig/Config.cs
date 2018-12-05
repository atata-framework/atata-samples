using System.Configuration;

namespace AtataSamples.AppConfig
{
    public static class Config
    {
        public static string BaseUrl { get; } = ConfigurationManager.AppSettings[nameof(BaseUrl)];

        public static class Account
        {
            public static string Email { get; } = ConfigurationManager.AppSettings[nameof(Email)];

            public static string Password { get; } = ConfigurationManager.AppSettings[nameof(Password)];
        }
    }
}
