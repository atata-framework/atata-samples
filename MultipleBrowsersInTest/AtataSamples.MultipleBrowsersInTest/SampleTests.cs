namespace AtataSamples.MultipleBrowsersInTest;

public sealed class SampleTests : MultiBrowserUITestFixture
{
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
}
