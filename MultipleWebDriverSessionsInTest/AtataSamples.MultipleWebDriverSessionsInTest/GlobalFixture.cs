namespace AtataSamples.MultipleBrowsersInTest;

public sealed class GlobalFixture : AtataGlobalFixture
{
    // Increase the minimum number of ThreadPool threads to improve parallel test execution with multiple browsers.
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
