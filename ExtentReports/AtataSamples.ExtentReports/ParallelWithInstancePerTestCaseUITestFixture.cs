namespace AtataSamples.ExtentReports;

[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class ParallelWithInstancePerTestCaseUITestFixture
{
    private static readonly ConcurrentDictionary<string, AtataContext> s_testIdAtataContextMap = new();

    [OneTimeSetUp]
    public static void InitFixtureContext() =>
        s_testIdAtataContextMap[TestContext.CurrentContext.Test.ID] = AtataContext.Configure()
            .UseDriverInitializationStage(AtataContextDriverInitializationStage.OnDemand)
            .Build();

    [OneTimeTearDown]
    public static void DisposeFixtureContext()
    {
        if (s_testIdAtataContextMap.TryRemove(TestContext.CurrentContext.Test.ID, out var fixtureContext))
            fixtureContext.Dispose();
    }

    [SetUp]
    public void SetUp() =>
        AtataContext.Configure().Build();

    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.Dispose();
}
