namespace AtataSamples.ExtJS;

[SetUpFixture]
public class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        AtataContext.GlobalConfiguration
            .UseChrome()
                .WithArguments("start-maximized", "disable-search-engine-choice-screen")
            .UseBaseUrl("https://demo.atata.io/")
            .UseCulture("en-US")
            .UseAllNUnitFeatures()
            .UseElementFindTimeout(TimeSpan.FromSeconds(15));

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }
}
