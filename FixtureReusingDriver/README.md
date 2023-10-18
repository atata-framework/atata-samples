# [Atata Samples](https://github.com/atata-framework/atata-samples) / Fixture Reusing Driver

[![Download sources](https://img.shields.io/badge/Download-sources-brightgreen.svg)](https://github.com/atata-framework/atata-samples/raw/master/_archives/FixtureReusingDriver.zip)

Demonstrates how to configure Atata to reuse the same driver instance by the tests in a fixture.

*[Download sources](https://github.com/atata-framework/atata-samples/raw/master/_archives/FixtureReusingDriver.zip), run tests, check results and experiment with [Atata Framework](https://atata.io).*

## Implementation

### UITestFixture

The base `UITestFixture` class should have additional functionality to preserve and reuse driver.

```cs
using Atata;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AtataSamples.FixtureReusingDriver;

[TestFixture]
public class UITestFixture
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
```

Driver reusing functionality is optional here and is disabled by default.
`ReuseDriver` property is responsible for enabling/disabling this functionality.

### Test Fixture

```cs
using Atata;
using NUnit.Framework;

namespace AtataSamples.FixtureReusingDriver;

public class PlanTests : UITestFixture
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
```

Note the line `protected override bool ReuseDriver => true;` which is required to enable driver reusing functionality.

The code of tests is just regular, nothing special is needed here.