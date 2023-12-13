using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;

namespace AtataSamples.SauceLabs;

[SetUpFixture]
public sealed class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp() =>
        AtataContext.GlobalConfiguration
            .UseRemoteDriver()
                .WithRemoteAddress("https://ondemand.eu-central-1.saucelabs.com:443/wd/hub")
                .WithOptions(CreateSauceLabsDriverOptions)
            .UseBaseUrl("https://atata.io/")
            .UseCulture("en-US")
            .UseAllNUnitFeatures()
            .ScreenshotConsumers.AddFile()
            .EventSubscriptions.Add<AtataContextDeInitEvent>(OnAtataContextDeInit);

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
            ["build"] = $"AtataSamples.SauceLabs / {AtataContext.BuildStartUtc:yyyyMMddTHHmmss}",
            ["name"] = AtataContext.Current.Test.FullName
        };

        browserOptions.AddAdditionalOption("sauce:options", sauceOptions);

        return browserOptions;
    }

    private static string GetRequiredEnvironmentVariable(string variableName) =>
        Environment.GetEnvironmentVariable(variableName)
            ?? throw new InvalidOperationException($"'{variableName}' environment variable is not found.");

    private static void OnAtataContextDeInit(AtataContextDeInitEvent eventData)
    {
        if (eventData.Context.HasDriver)
        {
            var isPassed = TestContext.CurrentContext.Result.Outcome.Status is not TestStatus.Warning or TestStatus.Failed;

            eventData.Context.Driver.AsScriptExecutor().ExecuteScript(
                $"sauce:job-result={(isPassed ? "passed" : "failed")}");
        }
    }
}
