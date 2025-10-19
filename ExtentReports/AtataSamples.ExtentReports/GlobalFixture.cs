namespace AtataSamples.ExtentReports;

public sealed class GlobalFixture : AtataGlobalFixture
{
    protected override void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder)
    {
        builder.Sessions.AddWebDriver(x => x
            .UseStartScopes(AtataContextScopes.Test)
            .UseChrome(x => x
                .WithArguments(
                    "headless=new",
                    "window-size=1024,768",
                    "disable-search-engine-choice-screen"))
            .UseBaseUrl("https://demo.atata.io/"));

        builder.LogConsumers.AddNLogFile();
        builder.UseExtentReports();
    }

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder) =>
        builder.SetUpWebDriversForUse();
}
