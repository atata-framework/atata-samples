using Atata;
using NUnit.Framework;

namespace AtataSamples.MultipleBrowsersViaFixtureArguments;

[TestFixture(DriverAliases.Chrome)]
[TestFixture(DriverAliases.Edge)]
////[TestFixture(DriverAliases.Firefox)]
////[TestFixture("chrome_remote")]
[Parallelizable]
public abstract class UITestFixture
{
    private readonly string _driverAlias;

    protected UITestFixture(string driverAlias) =>
        _driverAlias = driverAlias;

    [SetUp]
    public void SetUp() =>
        AtataContext.Configure()
            .UseDriver(_driverAlias)
            .UseTestName(() => $"[{_driverAlias}]{TestContext.CurrentContext.Test.Name}")
            .Build();

    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.Dispose();
}
