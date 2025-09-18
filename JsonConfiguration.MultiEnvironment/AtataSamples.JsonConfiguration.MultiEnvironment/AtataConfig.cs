namespace AtataSamples.JsonConfiguration.MultiEnvironment;

public class AtataConfig : JsonConfig<AtataConfig>
{
    public string AccountEmail { get; set; }

    public string AccountPassword { get; set; }
}
