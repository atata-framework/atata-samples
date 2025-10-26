namespace AtataSamples.MultipleBrowsersViaRunSettings;

public sealed class GlobalFixture : AtataGlobalFixture
{
    protected override void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder)
    {
        string driverAlias = TestContext.Parameters.Get("DriverAlias", WebDriverAliases.Chrome);

        builder.Sessions.AddWebDriver(x => x
            .UseStartScopes(AtataContextScopes.Test)
            .ConfigureChrome(x => x
                .WithArguments(
                    "start-maximized",
                    "disable-search-engine-choice-screen")
                .WithArtifactsAsDownloadDirectory())
            .ConfigureChrome("chrome-headless", x => x
                .WithArguments(
                    "headless=new",
                    "window-size=1920,1080",
                    "disable-search-engine-choice-screen")
                .WithArtifactsAsDownloadDirectory())
            .ConfigureFirefox()
            .UseDriver(driverAlias)
            .UseBaseUrl("https://demo.atata.io/"));
    }

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder) =>
        builder.SetUpWebDriversForUse();
}
