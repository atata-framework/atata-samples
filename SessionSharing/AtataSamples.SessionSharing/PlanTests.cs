namespace AtataSamples.SessionSharing;

//// 👇 Specifies that tests of this suite should not run in parallel with each other,
//// because they share the same WebDriverSession instance.
[Parallelizable(ParallelScope.Self)]
//// 👇 Starts a single WebDriverSession for suite and shares it with tests.
[StartSessionAndShare(typeof(WebDriverSession))]
public sealed class PlanTests : AtataTestSuite
{
    private PlansPage _page = null!;

    [OneTimeSetUp]
    public void SetUpSuite() =>
        _page = Go.To<PlansPage>();

    [Test]
    public void Plans_HasCorrectHeader() =>
        _page.AggregateAssert(x => x
            .PageTitle.Should.StartWith("Plans")
            .Header.Should.Be("Plans")
            .Content.Should.Contain("Please choose your payment plan"));

    [TestCase("Basic")]
    [TestCase("Plus")]
    [TestCase("Premium")]
    public void Plans_Has(string planTitle) =>
        _page.PlanItems[x => x.Title == planTitle].Should.BeVisible();
}
