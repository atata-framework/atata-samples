using System;
using System.Configuration;
using System.Reflection;

namespace AtataSamples.AppConfig
{
    public static class Config
    {
        private static readonly Lazy<Configuration> s_lazyConfiguration = new Lazy<Configuration>(
            () => ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location));

        public static string BaseUrl => GetAppSetting(nameof(BaseUrl));

        private static string GetAppSetting(string key) =>
            s_lazyConfiguration.Value.AppSettings.Settings[key].Value;

        public static class Account
        {
            public static string Email => GetAppSetting(nameof(Email));

            public static string Password => GetAppSetting(nameof(Password));
        }
    }
}
