using Atata;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace AtataSamples.SpecFlow;

[Binding]
public sealed class SpecFlowHooks
{
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

    [BeforeScenario]
    public void SetUpScenario() =>
        AtataContext.Configure()
            .EventSubscriptions.Add<ArtifactAddedEvent>(eventData => _outputHelper.AddAttachment(eventData.AbsoluteFilePath))
            .LogConsumers.Add(new TextOutputLogConsumer(_outputHelper.WriteLine))
            .Build();

    [AfterScenario]
    public static void TearDownScenario() =>
        AtataContext.Current?.Dispose();
}
