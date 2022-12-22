using Atata;
using NUnit.Framework;

namespace AtataSamples.ParallelTestsReusingDrivers;

public class CalculationTests : UITestFixture
{
    protected override DriverPoolUsage DriverPoolUsage => DriverPoolUsage.Global;

    [TestCase(1, 1, ExpectedResult = 2)]
    [TestCase(5, -5, ExpectedResult = 0)]
    [TestCase(100, 100, ExpectedResult = 200)]
    [TestCase(500, 1, ExpectedResult = 501)]
    [TestCase(1000, 1000, ExpectedResult = 2000)]
    [TestCase(1000, -2000, ExpectedResult = -1000)]
    [TestCase(10000, 10000, ExpectedResult = 20000)]
    public int Addition(int value1, int value2) =>
        Go.To<CalculationsPage>()
            .AdditionValue1.Set(value1)
            .AdditionValue2.Set(value2)
            .AdditionResult.Value;
}
