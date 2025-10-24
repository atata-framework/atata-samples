namespace AtataSamples.SalesforceLightning;

public sealed class GlobalFixture : AtataGlobalFixture
{
    protected override void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder) =>
        builder.Sessions.AddWebDriver(x => x
            .UseStartScopes(AtataContextScopes.Test)
            .UseChrome(x => x
                .WithArguments(
                    "start-maximized",
                    "disable-search-engine-choice-screen"))
            .UseBaseUrl("https://developer.salesforce.com/docs/component-library/bundle/"));

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder) =>
        builder.SetUpWebDriversForUse();
}
