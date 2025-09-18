namespace AtataSamples.MultipleBrowsersInTest;

[SetUpFixture]
public sealed class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        AtataContext.GlobalConfiguration
            .UseChrome()
                .WithArguments("window-size=1024,768", "disable-search-engine-choice-screen")
            .UseBaseUrl("https://demo.atata.io/")
            .UseCulture("en-US")
            .UseAllNUnitFeatures();

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }
}
