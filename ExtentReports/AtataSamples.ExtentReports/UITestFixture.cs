namespace AtataSamples.ExtentReports;

[Parallelizable(ParallelScope.Fixtures)]
public class UITestFixture
{
    protected AtataContext FixtureContext { get; set; }

    protected virtual bool UseFixtureDriverForTests => false;

    [OneTimeSetUp]
    public void InitFixtureContext() =>
        FixtureContext = AtataContext.Configure()
            .UseDriverInitializationStage(AtataContextDriverInitializationStage.OnDemand)
            .Build();

    [OneTimeTearDown]
    public void DisposeFixtureContext() =>
        FixtureContext?.Dispose();

    [SetUp]
    public void SetUp()
    {
        var testContextBuilder = AtataContext.Configure();

        if (UseFixtureDriverForTests)
            testContextBuilder
                .UseDriver(() => FixtureContext.Driver)
                .UseDisposeDriver(false);

        testContextBuilder.Build();
    }

    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.Dispose();
}
