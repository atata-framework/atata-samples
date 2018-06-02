using Atata.Configuration.Json;

namespace AtataSamples.JsonConfiguration.MultiEnvironment
{
    public class AppConfig : JsonConfig<AppConfig>
    {
        public string AccountEmail { get; set; }

        public string AccountPassword { get; set; }
    }
}
