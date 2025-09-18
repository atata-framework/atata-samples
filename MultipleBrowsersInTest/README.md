# [Atata Samples](https://github.com/atata-framework/atata-samples) / Multiple Browsers in Test

[![Download sources](https://img.shields.io/badge/Download-sources-brightgreen.svg)](https://github.com/atata-framework/atata-samples/raw/main/_archives/MultipleBrowsersInTest.zip)

Demonstrates the usage of multiple browsers in a single test.

*[Download sources](https://github.com/atata-framework/atata-samples/raw/main/_archives/MultipleBrowsersInTest.zip), run tests, check results and experiment with [Atata Framework](https://atata.io).*

## Implementation

### MultiBrowserUITestFixture

```cs
using System.Collections.Generic;
using Atata;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AtataSamples.MultipleBrowsersInTest;

[TestFixture]
public class MultiBrowserUITestFixture
{
    protected List<AtataContext> Contexts { get; } = new List<AtataContext>();

    [SetUp]
    public void SetUp() =>
        CreateContext();

    protected AtataContext CreateContext()
    {
        AtataContextBuilder contextBuilder = AtataContext.Configure()
            .EventSubscriptions.Add<AtataContextDeInitCompletedEvent>(e => Contexts.Remove(e.Context));

        if (AtataContext.Current is not null)
            contextBuilder.UseTestName($"{AtataContext.Current.Test.Name}[{Contexts.Count}]");

        AtataContext context = contextBuilder.Build();

        Contexts.Add(context);

        return context;
    }

    protected void SwitchToContext(int indexOfContext) =>
        SwitchToContext(Contexts[indexOfContext]);

    protected void SwitchToContext(AtataContext context)
    {
        int currentContextIndex = Contexts.IndexOf(AtataContext.Current);
        AtataContext.Current.Log.Info($"Switching from context #{currentContextIndex}");

        context.SetAsCurrent();

        int targetContextIndex = Contexts.IndexOf(context);
        AtataContext.Current.Log.Info($"Switched to context #{targetContextIndex}");
    }

    [TearDown]
    public void TearDown()
    {
        foreach (var context in Contexts.ToArray())
        {
            context.Dispose();
        }
    }
}

```

### Test

```cs
[Test]
public void MultipleBrowsersInTest()
{
    // Navigate to "Products" page in scope of the first context's browser.
    var productsPage = Go.To<OrdinaryPage>(url: "products")
        .PageTitle.Should.Contain("Products"); // Verify its title.

    // Create second context with new browser instance and automatically switch to it.
    CreateContext();

    // Navigate to "Calculations" page in scope of the second context's browser.
    var calculationsPage = Go.To<OrdinaryPage>(url: "calculations")
        .PageTitle.Should.Contain("Calculations"); // Verify its title.

    // Switch back to the first browser context.
    SwitchToContext(0);

    productsPage.PageTitle.Should.Contain("Products");

    // Switch to the second browser context.
    SwitchToContext(1);

    calculationsPage.PageTitle.Should.Contain("Calculations");
}
```
