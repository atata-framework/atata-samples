namespace AtataSamples.ParallelTestsReusingDrivers;

[Parallelizable(ParallelScope.All)]
public abstract class UITestFixture
{
    protected virtual DriverPoolUsage DriverPoolUsage =>
        DriverPoolUsage.None;

    [SetUp]
    public void SetUp() =>
        ConfigureAtataContext(AtataContext.Configure())
            .Build();

    protected virtual AtataContextBuilder ConfigureAtataContext(AtataContextBuilder builder)
    {
        if (DriverPoolUsage == DriverPoolUsage.Fixture)
            return builder.UseDriverPool(this);
        else if (DriverPoolUsage == DriverPoolUsage.Global)
            return builder.UseDriverPool();
        else
            return builder;
    }

    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.Dispose();

    [OneTimeTearDown]
    public void TearDownFixture()
    {
        if (DriverPoolUsage == DriverPoolUsage.Fixture)
            DriverPool.DisposeAllForScope(this);
    }
}
