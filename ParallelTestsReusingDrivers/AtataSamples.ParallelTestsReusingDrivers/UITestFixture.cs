using Atata;
using NUnit.Framework;

namespace AtataSamples.ParallelTestsReusingDrivers;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class UITestFixture
{
    protected virtual DriverPoolUsage DriverPoolUsage =>
        DriverPoolUsage.None;

    /// <summary>Sets up test a test.</summary>
    /// <seealso cref="SetUpFixture.GlobalSetUp"/>
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

    /// <summary>
    /// Tears down a test.
    /// </summary>
    /// <seealso cref="SetUpFixture.GlobalTearDown"/>
    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.CleanUp(quitDriver: false);

    [OneTimeTearDown]
    public void TearDownFixture()
    {
        if (DriverPoolUsage == DriverPoolUsage.Fixture)
            DriverPool.CloseAllForScope(this);
    }
}
