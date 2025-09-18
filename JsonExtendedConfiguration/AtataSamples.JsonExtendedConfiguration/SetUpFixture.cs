using Microsoft.Extensions.Configuration;

namespace AtataSamples.JsonExtendedConfiguration;

[SetUpFixture]
public class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        string testEnvironmentAlias = TestContext.Parameters.Get("TestEnvironment", "local");
        string driverAlias = TestContext.Parameters.Get("DriverAlias", "chrome-headless");

        // Find information on AtataContext set-up on https://atata.io/getting-started/#set-up
        // Find information on Atata JSON configuration on https://github.com/atata-framework/atata-configuration-json
        AtataContext.GlobalConfiguration
            .ApplyJsonConfig<AtataConfig>()
            .ApplyJsonConfig<AtataConfig>(environmentAlias: testEnvironmentAlias)
            .UseDriver(driverAlias);

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();

        // Read and set configuration from TestContext:
        AtataConfig.Global.Username = TestContext.Parameters.Get("TestUsername", AtataConfig.Global.Username);
        AtataConfig.Global.Password = TestContext.Parameters.Get("TestPassword", AtataConfig.Global.Password);

        // Read and set configuration from environment variables:
        AtataConfig.Global.Username = Environment.GetEnvironmentVariable("TestUsername") ?? AtataConfig.Global.Username;
        AtataConfig.Global.Password = Environment.GetEnvironmentVariable("TestPassword") ?? AtataConfig.Global.Password;

        // Read and set configuration through ConfigurationBuilder from environment variables, user secrets, etc.:
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddUserSecrets<SetUpFixture>()
            .Build();

        configuration.Bind(AtataConfig.Global);

        // Another alternative approach is to add custom variables:
        AtataContext.GlobalConfiguration.AddVariable("key", "val");

        // You can get the value in the test this way:
        // AtataContext.Current.Variables["key"];

        // Additional recommendation is to mask secret strings in log:
        AtataContext.GlobalConfiguration.AddSecretStringToMaskInLog(AtataConfig.Global.Password);
    }
}
