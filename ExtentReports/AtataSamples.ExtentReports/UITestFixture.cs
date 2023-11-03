using Atata;
using Atata.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AtataSamples.ExtentReports;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class UITestFixture
{
    protected AtataContext FixtureContext { get; set; }

    protected virtual bool UseFixtureDriverForTests => false;

    [OneTimeSetUp]
    public void InitFixtureContext() =>
        FixtureContext = AtataContext.Configure()
            .UseDriverInitializationStage(AtataContextDriverInitializationStage.OnDemand)
            .LogConsumers.Add<ExtentLogConsumer>()
                .WithMinLevel(LogLevel.Warn)
            .Build();

    [OneTimeTearDown]
    public void DisposeFixtureContext() =>
        FixtureContext?.Dispose();

    [SetUp]
    public void SetUp()
    {
        var testContextBuilder = AtataContext.Configure()
            .LogConsumers.Add<ExtentLogConsumer>()
                .WithMinLevel(LogLevel.Info)
            .EventSubscriptions.Add(new AddArtifactsToExtentReportEventHandler());

        if (UseFixtureDriverForTests)
            testContextBuilder
                .UseDriver(FixtureContext.Driver)
                .UseDisposeDriver(false);

        testContextBuilder.Build();
    }

    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.Dispose();
}
