namespace AtataSamples.NUnit.AdvancedTestProject;

public sealed class GlobalConfig
{
    public required string BaseUrl { get; init; }

    public required string WebDriverAlias { get; init; } = "chrome-headless";

    // Other configuration properties can be added here.
}
