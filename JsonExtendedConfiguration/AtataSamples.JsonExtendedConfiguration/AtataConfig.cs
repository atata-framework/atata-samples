using Atata.Configuration.Json;

namespace AtataSamples.JsonExtendedConfiguration;

public class AtataConfig : JsonConfig<AtataConfig>
{
    public string Username { get; set; }

    public string Password { get; set; }
}
