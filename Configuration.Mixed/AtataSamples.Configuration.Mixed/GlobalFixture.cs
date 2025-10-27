using Microsoft.Extensions.Configuration;

namespace AtataSamples.Configuration.Mixed;

public sealed class GlobalFixture : AtataGlobalFixture
{
    private GlobalConfig? _config;

    protected override void OnBeforeGlobalSetup()
    {
        string environmentName = TestContext.Parameters.Get("TestEnvironment", defaultValue: "local");

        // Read configuration through ConfigurationBuilder from environment variables, user secrets, etc.:
        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"config.{environmentName}.json")
            .AddEnvironmentVariables()
            .AddUserSecrets<GlobalFixture>()
            .Build();

        _config = configuration.Get<GlobalConfig>()!;

        // Alternatively read configuration from TestContext:
        _config.Username = TestContext.Parameters.Get("TestUsername", _config.Username);
        _config.Password = TestContext.Parameters.Get("TestPassword", _config.Password);

        // Alternatively read configuration from environment variables directly:
        _config.Username ??= Environment.GetEnvironmentVariable("TestUsername")!;
        _config.Password ??= Environment.GetEnvironmentVariable("TestPassword")!;
    }

    protected override void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder)
    {
        builder.Sessions.AddWebDriver(x => x
            .UseStartScopes(AtataContextScopes.Test)
            .ConfigureChrome("chrome-headed", x => x
                .WithArguments(
                    "start-maximized",
                    "disable-search-engine-choice-screen")
                .WithArtifactsAsDownloadDirectory())
            .ConfigureChrome("chrome-headless", x => x
                .WithArguments(
                    "headless=new",
                    "window-size=1920,1080",
                    "disable-search-engine-choice-screen")
                .WithArtifactsAsDownloadDirectory())
            .UseDriver(_config!.WebDriverAlias)
            .UseBaseUrl(_config!.BaseUrl));

        // Additional recommendation is to mask secret strings in log:
        builder.AddSecretStringToMaskInLog(_config!.Password);
    }

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder)
    {
        // You can put the whole configuration object into Atata state for easy access in tests:
        builder.UseState(_config);

        // Also you can put some of the configuration properties to Atata variables:
        builder.UseVariable("key", "val");

        // You can get the value in a test this way:
        //// string? value = Context.Variables["key"];

        builder.SetUpWebDriversForUse();
    }
}
