namespace AtataSamples.Configuration.MultiEnvironmentViaRunSettings;

public sealed class GlobalConfig
{
    public required string BaseUrl { get; init; }

    public required string AccountEmail { get; init; }

    public required string AccountPassword { get; init; }

    public required string WebDriverAlias { get; init; }

    public static GlobalConfig CreateLocal() =>
        new()
        {
            BaseUrl = "https://demo.atata.io/",
            AccountEmail = "admin@mail.com",
            AccountPassword = "admin",
            WebDriverAlias = "chrome-headed"
        };
}
