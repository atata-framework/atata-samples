using Microsoft.Extensions.Configuration;

namespace AtataSamples.Configuration.MultiEnvViaRunSettingsAndJson;

public sealed class GlobalFixture : AtataGlobalFixture
{
    private GlobalConfig? _config;

    protected override void OnBeforeGlobalSetup()
    {
        string environmentName = TestContext.Parameters.Get("TestEnvironment", defaultValue: "local");

        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"config.{environmentName}.json")
            .AddEnvironmentVariables()
            .Build();

        _config = configuration.Get<GlobalConfig>();
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

        builder.LogConsumers.AddNLogFile();
    }

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder)
    {
        builder.UseState(_config);

        builder.SetUpWebDriversForUse();
    }
}
