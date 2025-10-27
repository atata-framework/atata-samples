namespace AtataSamples.JsonExtendedConfiguration;

public sealed class GlobalConfig
{
    public required string BaseUrl { get; init; }

    public required string WebDriverAlias { get; init; } = "chrome-headless";

    public required string Username { get; set; }

    public required string Password { get; set; }
}
