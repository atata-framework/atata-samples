namespace AtataSamples.FixtureReusingDriver;

public abstract class UITestFixture
{
    protected virtual bool ReuseDriver => false;

    protected IWebDriver PreservedDriver { get; private set; }

    [OneTimeSetUp]
    public void SetUpFixture()
    {
        if (ReuseDriver)
            PreservedDriver = AtataContext.GlobalConfiguration.BuildingContext.DriverFactoryToUse.Create();
    }

    [SetUp]
    public void SetUp()
    {
        AtataContextBuilder contextBuilder = AtataContext.Configure();

        if (ReuseDriver && PreservedDriver is not null)
            contextBuilder
                .UseDriver(PreservedDriver)
                .UseDisposeDriver(false);

        contextBuilder.Build();
    }

    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.Dispose();

    [OneTimeTearDown]
    public void TearDownFixture()
    {
        if (PreservedDriver is not null)
        {
            PreservedDriver.Dispose();
            PreservedDriver = null;
        }
    }
}
