namespace AtataSamples.JsonExtendedConfiguration;

public sealed class AtataConfig : JsonConfig<AtataConfig>
{
    public string Username { get; set; }

    public string Password { get; set; }
}
