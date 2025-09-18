namespace AtataSamples.JsonConfiguration.MultiEnvironment;

[SetUpFixture]
public sealed class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        string testEnvironmentAlias = TestContext.Parameters.Get("TestEnvironment", "local");
        string driverAlias = TestContext.Parameters.Get("DriverAlias", DriverAliases.Chrome);

        AtataContext.GlobalConfiguration
            .ApplyJsonConfig<AtataConfig>()
            .ApplyJsonConfig<AtataConfig>(environmentAlias: testEnvironmentAlias)
            .AddSecretStringToMaskInLog(AtataConfig.Global.AccountPassword)
            .UseDriver(driverAlias);

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }
}
