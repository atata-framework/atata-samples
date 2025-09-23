# [Atata Samples](https://github.com/atata-framework/atata-samples) / Fixture Reusing Driver

[![Download sources](https://img.shields.io/badge/Download-sources-brightgreen.svg)](https://github.com/atata-framework/atata-samples/raw/main/_archives/FixtureReusingDriver.zip)

Demonstrates how to configure Atata to reuse the same driver instance by the tests in a fixture.

*[Download sources](https://github.com/atata-framework/atata-samples/raw/main/_archives/FixtureReusingDriver.zip), run tests, check results and experiment with [Atata Framework](https://atata.io).*

## Implementation

In order to reuse the session by the tests in a fixture, `[StartSessionAndShare(typeof(...))]` is needed.
`[Parallelizable(ParallelScope.Self)]` is an NUnit-specific attribute, which is optional.

```cs
namespace AtataSamples.FixtureReusingDriver;

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
        _page
            .AggregateAssert(x => x
                .PageTitle.Should.StartWith("Plans")
                .Header.Should.Be("Plans")
                .Content.Should.Contain("Please choose your payment plan"));

    [TestCase("Basic")]
    [TestCase("Plus")]
    [TestCase("Premium")]
    public void Plans_Has(string planTitle) =>
        _page
            .PlanItems[x => x.Title == planTitle].Should.BeVisible();
}
```

If all the tests are executed on the same page and the page is not changing,
then it is possible to navigate to that page only once in the `[OneTimeSetUp]` method
of the suite, like here:

```cs
private PlansPage _page = null!;

[OneTimeSetUp]
public void SetUpSuite() =>
    _page = Go.To<PlansPage>();
```
