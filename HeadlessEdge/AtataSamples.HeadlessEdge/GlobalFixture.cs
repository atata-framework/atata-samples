namespace AtataSamples.HeadlessEdge;

public sealed class GlobalFixture : AtataGlobalFixture
{
    protected override void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder) =>
        builder.Sessions.AddWebDriver(x => x
            .UseStartScopes(AtataContextScopes.Test)
            .UseEdge(x => x
                .WithArguments(
                    "headless=new",
                    "window-size=1024,768"))
            .UseBaseUrl("https://demo.atata.io/"));

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder) =>
        builder.EventSubscriptions.Add(SetUpWebDriversForUseEventHandler.Instance);
}
