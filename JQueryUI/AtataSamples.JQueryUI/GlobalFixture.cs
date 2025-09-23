namespace AtataSamples.JQueryUI;

public sealed class GlobalFixture : AtataGlobalFixture
{
    protected override void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder) =>
        builder.Sessions.AddWebDriver(x => x
            .UseStartScopes(AtataContextScopes.Test)
            .UseEdge(x => x
                .WithArguments(
                    "start-maximized",
                    "disable-search-engine-choice-screen"))
            .UseBaseUrl("https://jqueryui.com/"));

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder) =>
        builder.EventSubscriptions.Add(SetUpWebDriversForUseEventHandler.Instance);
}
