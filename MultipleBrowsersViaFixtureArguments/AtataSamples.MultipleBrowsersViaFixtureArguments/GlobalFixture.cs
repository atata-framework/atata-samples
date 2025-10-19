using OpenQA.Selenium.Chrome;

namespace AtataSamples.MultipleBrowsersViaFixtureArguments;

public sealed class GlobalFixture : AtataGlobalFixture
{
    protected override void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder)
    {
        builder.Sessions.AddWebDriver(x => x
            .UseStartScopes(AtataContextScopes.Test)
            .ConfigureChrome(x => x
                .WithArguments(
                    "start-maximized",
                    "disable-search-engine-choice-screen"))
            .ConfigureEdge(x => x
                .WithArguments(
                    "start-maximized",
                    "disable-search-engine-choice-screen"))
            .ConfigureFirefox()
            .ConfigureRemoteDriver("chrome-remote", x => x
                .WithRemoteAddress("http://127.0.0.1:8888/wd/hub")
                .WithOptions(new ChromeOptions
                {
                    // TODO: Set specific options.
                }))
            .UseBaseUrl("https://demo.atata.io/"));

        builder.LogConsumers.AddNLogFile();
    }

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder) =>
        builder.SetUpWebDriversConfigured();
}
