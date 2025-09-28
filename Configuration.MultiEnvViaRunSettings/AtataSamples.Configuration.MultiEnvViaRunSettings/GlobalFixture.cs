using Microsoft.Extensions.Configuration;

namespace AtataSamples.Configuration.MultiEnvViaRunSettings;

public sealed class GlobalFixture : AtataGlobalFixture
{
    private readonly GlobalConfig _config = GlobalConfig.CreateLocal();

    protected override void ConfigureAtataContextGlobalProperties(AtataContextGlobalProperties globalProperties) =>
        LoadConfiguration();

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
            .UseDriver(_config.WebDriverAlias)
            .UseBaseUrl(_config.BaseUrl));

        builder.LogConsumers.AddNLogFile();
    }

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder)
    {
        builder.UseState(_config);

        builder.EventSubscriptions.Add(SetUpWebDriversForUseEventHandler.Instance);
    }

    private void LoadConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        configuration.Bind(_config);
    }
}
