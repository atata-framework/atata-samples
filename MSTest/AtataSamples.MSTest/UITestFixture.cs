namespace AtataSamples.MSTest;

public abstract class UITestFixture
{
    public TestContext TestContext { get; set; }

    [TestInitialize]
    public void SetUp() =>
        AtataContext.Configure()
            .UseTestName(TestContext.TestName)
            .UseTestSuiteType(GetType())
            .LogConsumers.Add(new TextOutputLogConsumer(TestContext.WriteLine))
            .Build();

    [TestCleanup]
    public void TearDown() =>
        AtataContext.Current?.Dispose();

    protected static void Execute(Action action)
    {
        try
        {
            action?.Invoke();
        }
        catch (Exception exception)
        {
            OnException(exception);
            throw;
        }
    }

    private static void OnException(Exception exception)
    {
        var context = AtataContext.Current;

        context.Log.Error(exception, null);

        context.TakeScreenshot("Failed");
        context.TakePageSnapshot("Failed");
    }
}
