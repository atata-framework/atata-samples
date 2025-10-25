using OpenQA.Selenium.Chrome;

namespace AtataSamples.SauceLabs;

public sealed class GlobalFixture : AtataGlobalFixture
{
    protected override void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder) =>
        builder.Sessions.AddWebDriver(x => x
            .UseStartScopes(AtataContextScopes.Test)
            .UseRemoteDriver(x => x
                .WithRemoteAddress("https://ondemand.eu-central-1.saucelabs.com:443/wd/hub")
                .WithOptions(CreateSauceLabsDriverOptions))
            .UseBaseUrl("https://atata.io/")
            .EventSubscriptions.Add<AtataContextDeInitStartedEvent>(OnAtataContextDeInit));

    private static DriverOptions CreateSauceLabsDriverOptions()
    {
        var browserOptions = new ChromeOptions
        {
            PlatformName = "Windows 11",
            BrowserVersion = "latest"
        };

        var sauceOptions = new Dictionary<string, object>
        {
            ["username"] = GetRequiredEnvironmentVariable("SAUCE_USERNAME"),
            ["accessKey"] = GetRequiredEnvironmentVariable("SAUCE_ACCESS_KEY"),
            ["build"] = $"AtataSamples.SauceLabs / {AtataContext.GlobalProperties.RunStart:yyyyMMddTHHmmss}",
            ["name"] = AtataContext.ResolveCurrent().Test.FullName!
        };

        browserOptions.AddAdditionalOption("sauce:options", sauceOptions);

        return browserOptions;
    }

    private static string GetRequiredEnvironmentVariable(string variableName) =>
        Environment.GetEnvironmentVariable(variableName)
            ?? throw new InvalidOperationException($"'{variableName}' environment variable is not found.");

    private static void OnAtataContextDeInit(AtataContextDeInitStartedEvent eventData)
    {
        if (eventData.Context.Sessions.TryGet<WebDriverSession>(out var webDriverSession))
        {
            var isPassed = eventData.Context.TestResultStatus == TestResultStatus.Passed;

            webDriverSession.Driver.AsScriptExecutor().ExecuteScript(
                $"sauce:job-result={(isPassed ? "passed" : "failed")}");
        }
    }
}
