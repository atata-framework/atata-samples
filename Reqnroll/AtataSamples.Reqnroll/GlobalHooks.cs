namespace AtataSamples.Reqnroll;

[Binding]
public sealed class GlobalHooks
{
    private const string UsesSingleWebSessionTag = "UsesSingleWebSession";

    [BeforeFeature]
    public static void SetUpFeature(FeatureContext featureContext) =>
        ReqnrollAtataContextSetup.SetUpFeature(featureContext, ConfigureFeatureAtataContext);

    [AfterFeature]
    public static void TearDownFeature(FeatureContext featureContext) =>
        ReqnrollAtataContextSetup.TearDownFeature(featureContext);

    [BeforeScenario]
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    public void SetUpScenario(FeatureContext featureContext, ScenarioContext scenarioContext) =>
        ReqnrollAtataContextSetup.SetUpScenario(featureContext, scenarioContext, ConfigureScenarioAtataContext);

    [AfterScenario]
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    public void TearDownScenario(ScenarioContext scenarioContext) =>
        ReqnrollAtataContextSetup.TearDownScenario(scenarioContext);

    private static void ConfigureFeatureAtataContext(
        AtataContextBuilder builder,
        FeatureContext featureContext)
    {
        if (featureContext.FeatureInfo.Tags.Contains(UsesSingleWebSessionTag))
        {
            builder.Sessions.ConfigureWebDriver(x => x
                .UseStart(true)
                .UseAsShared());
        }

        // TODO: Add extra configuration for feature AtataContext, or remove the comment.
    }

    private static void ConfigureScenarioAtataContext(
        AtataContextBuilder builder,
        FeatureContext featureContext,
        ScenarioContext scenarioContext)
    {
        if (scenarioContext.ScenarioInfo.CombinedTags.Contains(UsesSingleWebSessionTag))
        {
            builder.Sessions.ConfigureWebDriver(x => x
                .UseStart(false));
            builder.Sessions.Borrow<WebDriverSession>();
        }

        // TODO: Add extra configuration for scenario AtataContext, or remove the comment.
    }
}
