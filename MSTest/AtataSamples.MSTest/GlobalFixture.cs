namespace AtataSamples.MSTest;

[TestClass]
public static class GlobalFixture
{
    [AssemblyInitialize]
    public static void GlobalSetUp(TestContext testContext)
    {
        ConfigureAtataContextBaseConfiguration(AtataContext.BaseConfiguration);

        MSTestGlobalAtataContextSetup.SetUp(typeof(GlobalFixture), testContext, ConfigureGlobalAtataContext);
    }

    [AssemblyCleanup]
    public static void TearDownAssembly(TestContext testContext) =>
        MSTestGlobalAtataContextSetup.TearDown(testContext);

    private static void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder) =>
        builder.Sessions.AddWebDriver(x => x
            .UseStartScopes(AtataContextScopes.Test)
            .UseChrome(x => x
                .WithArguments(
                    "start-maximized",
                    "disable-search-engine-choice-screen"))
            .UseBaseUrl("https://demo.atata.io/"));

    private static void ConfigureGlobalAtataContext(AtataContextBuilder builder) =>
        builder.SetUpWebDriversForUse();
}
