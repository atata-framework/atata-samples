namespace AtataSamples.FixtureReusingDriver;

public sealed class PlanTests : UITestFixture
{
    protected override bool ReuseDriver => true;

    [Test]
    public void Plans_HasCorrectHeader() =>
        Go.To<PlansPage>()
            .AggregateAssert(x => x
                .PageTitle.Should.StartWith("Plans")
                .Header.Should.Equal("Plans")
                .Content.Should.Contain("Please choose your payment plan"));

    [TestCase("Basic")]
    [TestCase("Plus")]
    [TestCase("Premium")]
    public void Plans_Has(string planTitle) =>
        Go.To<PlansPage>()
            .PlanItems[x => x.Title == planTitle].Should.BeVisible();
}
