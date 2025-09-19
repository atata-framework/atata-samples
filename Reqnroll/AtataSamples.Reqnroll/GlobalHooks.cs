namespace AtataSamples.Reqnroll;

[Binding]
public sealed class GlobalHooks
{
    private const string ReusesFeatureDriverTag = "ReusesFeatureDriver";

    private readonly IReqnrollOutputHelper _outputHelper;

    public GlobalHooks(IReqnrollOutputHelper outputHelper) =>
        _outputHelper = outputHelper;

    [BeforeTestRun]
    public static void SetUpTestRun()
    {
        AtataContext.GlobalConfiguration
            .UseChrome()
                .WithArguments("start-maximized", "disable-search-engine-choice-screen")
            .UseBaseUrl("https://demo.atata.io/")
            .UseCulture("en-US")
            .UseSpecFlowNUnitFeatures();

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }

    [BeforeFeature]
    public static void SetUpFeature(FeatureContext featureContext)
    {
        AtataContext featureAtataContext = AtataContext.Configure()
            .UseDriverInitializationStage(AtataContextDriverInitializationStage.OnDemand)
            .Build();

        featureContext.Set(featureAtataContext);
    }

    [AfterFeature]
    public static void TearDownFeature(FeatureContext featureContext)
    {
        if (featureContext.TryGetValue(out AtataContext featureAtataContext))
            featureAtataContext.Dispose();
    }

    [BeforeScenario]
    public void SetUpScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        var scenarioAtataContextBuilder = AtataContext.Configure()
            .EventSubscriptions.Add<ArtifactAddedEvent>(eventData => _outputHelper.AddAttachment(eventData.AbsoluteFilePath))
            .LogConsumers.Add(new TextOutputLogConsumer(_outputHelper.WriteLine));

        if (scenarioContext.ScenarioInfo.CombinedTags.Contains(ReusesFeatureDriverTag))
            scenarioAtataContextBuilder
                .UseDriver(() => featureContext.Get<AtataContext>().Driver)
                .UseDisposeDriver(false);

        AtataContext scenarioAtataContext = scenarioAtataContextBuilder.Build();
        scenarioContext.Set(scenarioAtataContext);
    }

    [AfterScenario]
    public static void TearDownScenario() =>
        AtataContext.Current?.Dispose();
}
