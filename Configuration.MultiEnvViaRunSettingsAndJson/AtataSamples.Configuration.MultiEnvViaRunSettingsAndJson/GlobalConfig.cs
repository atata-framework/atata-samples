namespace AtataSamples.Configuration.MultiEnvViaRunSettingsAndJson;

public sealed class GlobalConfig
{
    public required string BaseUrl { get; init; }

    public required string AccountEmail { get; init; }

    public required string AccountPassword { get; init; }

    public required string WebDriverAlias { get; init; } = "chrome-headed";
}
