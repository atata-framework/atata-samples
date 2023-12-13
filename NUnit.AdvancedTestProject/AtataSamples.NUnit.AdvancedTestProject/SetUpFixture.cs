namespace AtataSamples.NUnit.AdvancedTestProject;

[SetUpFixture]
public sealed class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        string testEnvironmentAlias = TestContext.Parameters.Get("TestEnvironment", "local");
        string driverAlias = TestContext.Parameters.Get("DriverAlias", DriverAliases.Chrome);

        // Find information on AtataContext configuration on https://atata.io/getting-started/#configuration
        // Find information on Atata JSON configuration on https://github.com/atata-framework/atata-configuration-json
        AtataContext.GlobalConfiguration
            .ApplyJsonConfig<AtataConfig>()
            .ApplyJsonConfig<AtataConfig>(environmentAlias: testEnvironmentAlias)
            .UseDriver(driverAlias);

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }
}
