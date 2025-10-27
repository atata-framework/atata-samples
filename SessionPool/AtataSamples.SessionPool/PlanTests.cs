namespace AtataSamples.SessionPool;

//// 👇 Specifies that tests of this suite should not run in parallel with each other,
//// because they share the same WebDriverSession instance.
[Parallelizable(ParallelScope.Self)]
//// 👇 Starts a single WebDriverSession for suite and shares it with tests.
[TakeSessionFromPoolAndShare(typeof(WebDriverSession))]
public sealed class PlanTests : AtataTestSuite
{
    private PlansPage _page = null!;

    [OneTimeSetUp]
    public void SetUpSuite() =>
        _page = Go.To<PlansPage>();

    [TestCase("Basic")]
    [TestCase("Plus")]
    [TestCase("Premium")]
    public void Plans_Has(string title) =>
        _page.PlanItems[x => x.Title == title].Should.BeVisible();
}
