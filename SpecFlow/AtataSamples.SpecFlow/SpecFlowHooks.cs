using System.Linq;
using Atata;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace AtataSamples.SpecFlow;

[Binding]
public sealed class SpecFlowHooks
{
    private const string ReusesFeatureDriverTag = "ReusesFeatureDriver";

    private readonly ISpecFlowOutputHelper _outputHelper;

    public SpecFlowHooks(ISpecFlowOutputHelper outputHelper) =>
        _outputHelper = outputHelper;

    [BeforeTestRun]
    public static void SetUpTestRun()
    {
        AtataContext.GlobalConfiguration
            .UseChrome()
                .WithArguments("start-maximized")
            .UseBaseUrl("https://demo.atata.io/")
            .UseCulture("en-US")
            .UseNUnitTestName()
            .UseNUnitTestSuiteName()
            .UseNUnitTestSuiteType()
            .UseNUnitAssertionExceptionType()
            .UseNUnitAggregateAssertionStrategy()
            .UseNUnitWarningReportStrategy()
            .EventSubscriptions.LogNUnitError()
            .EventSubscriptions.TakeScreenshotOnNUnitError()
            .EventSubscriptions.TakePageSnapshotOnNUnitError()
            .ScreenshotConsumers.AddFile();

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

        if (scenarioContext.ScenarioInfo.ScenarioAndFeatureTags.Contains(ReusesFeatureDriverTag))
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
