namespace AtataSamples.AspNetCore.UITests;

public sealed class GlobalFixture : AtataGlobalFixture
{
    private const int WebApplicationKestrelPort = 7260;

    protected override void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder)
    {
        builder.Sessions.AddWebDriver(x => x
            .UseStartScopes(AtataContextScopes.Test)
            .AddDependentConfiguration<WebApplicationSession>((x, other) => x
                .UseBaseUrl(other.Uri))
            .UseChrome(x => x
                .WithArguments(
                    "headless=new",
                    "window-size=1920,1080",
                    "disable-search-engine-choice-screen")
                .WithArtifactsAsDownloadDirectory()));

        builder.LogConsumers.AddNLogFile(x => x
            .WithSeparateSourceLogFiles());
    }

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder)
    {
        builder.SetUpWebDriversForUse();

        builder.Sessions.AddWebApplication(x => x
           .Use<Program>()
           .UseKestrel(WebApplicationKestrelPort));
    }
}
