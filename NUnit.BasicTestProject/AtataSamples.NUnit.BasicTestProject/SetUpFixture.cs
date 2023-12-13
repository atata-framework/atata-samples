namespace AtataSamples.NUnit.BasicTestProject;

[SetUpFixture]
public sealed class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        // Find information about AtataContext configuration on https://atata.io/getting-started/#configuration
        AtataContext.GlobalConfiguration
            .UseChrome()
                .WithArguments("start-maximized")
            .UseBaseUrl("https://atata.io/")
            .UseCulture("en-US")
            .UseAllNUnitFeatures()
            .ScreenshotConsumers.AddFile();

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }
}
