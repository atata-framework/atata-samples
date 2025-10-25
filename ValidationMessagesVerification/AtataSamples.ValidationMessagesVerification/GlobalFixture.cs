namespace AtataSamples.ValidationMessagesVerification;

public sealed class GlobalFixture : AtataGlobalFixture
{
    protected override void OnBeforeGlobalSetup() =>
        ThreadPool.SetMinThreads(Environment.ProcessorCount * 4, Environment.ProcessorCount);

    protected override void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder) =>
        builder.Sessions.AddWebDriver(x => x
            .UseStartScopes(AtataContextScopes.Test)
            .UseChrome(x => x
                .WithArguments(
                    "start-maximized",
                    "disable-search-engine-choice-screen"))
            .UseBaseUrl("https://demo.atata.io/"));

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder) =>
        builder.SetUpWebDriversForUse();
}
