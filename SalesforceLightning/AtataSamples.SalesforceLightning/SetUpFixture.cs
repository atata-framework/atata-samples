namespace AtataSamples.SalesforceLightning;

[SetUpFixture]
public sealed class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        AtataContext.GlobalConfiguration
            .UseChrome()
                .WithArguments("start-maximized", "disable-search-engine-choice-screen")
            .UseBaseUrl("https://developer.salesforce.com/docs/component-library/bundle/")
            .UseCulture("en-US")
            .UseAllNUnitFeatures();

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }
}
