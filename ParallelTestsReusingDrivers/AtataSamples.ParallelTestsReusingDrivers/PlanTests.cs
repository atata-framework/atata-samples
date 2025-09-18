namespace AtataSamples.ParallelTestsReusingDrivers;

public sealed class PlanTests : UITestFixture
{
    protected override DriverPoolUsage DriverPoolUsage =>
        DriverPoolUsage.Global;

    [TestCase("Basic")]
    [TestCase("Plus")]
    [TestCase("Premium")]
    public void Plans_Has(string title) =>
        Go.To<PlansPage>()
            .PlanItems[x => x.Title == title].Should.BeVisible();
}
