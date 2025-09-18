namespace AtataSamples.AppConfig;

[SetUpFixture]
public sealed class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        AtataContext.GlobalConfiguration
            .UseChrome()
                .WithArguments("start-maximized", "disable-search-engine-choice-screen")
            .UseBaseUrl(Config.BaseUrl)
            .UseCulture("en-US")
            .UseAllNUnitFeatures();

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }
}
