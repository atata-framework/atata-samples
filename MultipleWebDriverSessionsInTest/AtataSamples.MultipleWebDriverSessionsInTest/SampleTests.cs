namespace AtataSamples.MultipleBrowsersInTest;

public sealed class SampleTests : AtataTestSuite
{
    [Test]
    [StartSession(typeof(WebDriverSession), Count = 2)] // Tells Atata to start 2 sessions automatically for this test.
    public void CreateTwoSessionsUsingAttribute_NavigateThroughSessions()
    {
        // Gets a first session.
        var session1 = Context.Sessions.Get<WebDriverSession>(0);

        // Gets a second session.
        var session2 = Context.Sessions.Get<WebDriverSession>(1);

        // Navigate to "Products" page in scope of the first session.
        var productsPage = session1.Go.To<OrdinaryPage>(url: "products")
            .PageTitle.Should.Contain("Products"); // Verify its title.

        // Navigate to "Calculations" page in scope of the second session.
        var calculationsPage = session2.Go.To<OrdinaryPage>(url: "calculations")
            .PageTitle.Should.Contain("Calculations"); // Verify its title.

        // Assert page title of the first session page.
        productsPage.PageTitle.Should.Contain("Products");

        // Assert page title of the second session page.
        calculationsPage.PageTitle.Should.Contain("Calculations");
    }

    [Test]
    [StartSession(typeof(WebDriverSession), Count = 2)] // Tells Atata to start 2 sessions automatically for this test.
    public void CreateTwoSessionsUsingAttribute_NavigateThroughGoToAfterManualSessionSwitch()
    {
        // Gets a first session.
        var session1 = Context.Sessions.Get<WebDriverSession>(0);

        // Gets a second session.
        var session2 = Context.Sessions.Get<WebDriverSession>(1);

        // Switch to the first session.
        session1.SetAsCurrent();

        // Navigate to "Products" page in scope of the first session.
        var productsPage = Go.To<OrdinaryPage>(url: "products")
            .PageTitle.Should.Contain("Products"); // Verify its title.

        // Switch to the second session.
        session2.SetAsCurrent();

        // Navigate to "Calculations" page in scope of the second session.
        var calculationsPage = Go.To<OrdinaryPage>(url: "calculations")
            .PageTitle.Should.Contain("Calculations"); // Verify its title.

        // Assert page title of the first session page.
        productsPage.PageTitle.Should.Contain("Products");

        // Assert page title of the second session page.
        calculationsPage.PageTitle.Should.Contain("Calculations");
    }

    [Test]
    public async Task CreateSecondSessionDuringTest_NavigateThroughGoToRelyingOnAutoSessionSwitch()
    {
        // Navigate to "Products" page in scope of the first session.
        var productsPage = Go.To<OrdinaryPage>(url: "products")
            .PageTitle.Should.Contain("Products"); // Verify its title.

        // Build a second session. It automatically becomes current.
        await Context.Sessions.BuildAsync<WebDriverSession>();

        // Navigate to "Calculations" page in scope of the second session.
        var calculationsPage = Go.To<OrdinaryPage>(url: "calculations")
            .PageTitle.Should.Contain("Calculations"); // Verify its title.

        // Assert page title of the first session page.
        productsPage.PageTitle.Should.Contain("Products");

        // Assert page title of the second session page.
        calculationsPage.PageTitle.Should.Contain("Calculations");
    }

    [Test]
    public async Task CreateSecondSessionDuringTest_NavigateThroughSessions()
    {
        // Get the first session, which is already started.
        var session1 = Context.Sessions.Get<WebDriverSession>();

        // Build a second session.
        var session2 = await Context.Sessions.BuildAsync<WebDriverSession>();

        // Navigate to "Products" page in scope of the first session.
        var productsPage = session1.Go.To<OrdinaryPage>(url: "products")
            .PageTitle.Should.Contain("Products"); // Verify its title.

        // Navigate to "Calculations" page in scope of the second session.
        var calculationsPage = session2.Go.To<OrdinaryPage>(url: "calculations")
            .PageTitle.Should.Contain("Calculations"); // Verify its title.

        // Assert page title of the first session page.
        productsPage.PageTitle.Should.Contain("Products");

        // Assert page title of the second session page.
        calculationsPage.PageTitle.Should.Contain("Calculations");
    }

    [Test]
    [DisableSession(typeof(WebDriverSession))] // Tells Atata not to start session automatically for this test.
    public async Task CreateTwoSessionsDuringTest_NavigateThroughSessions()
    {
        // Build a first session.
        var session1 = await Context.Sessions.BuildAsync<WebDriverSession>();

        // Build a second session.
        var session2 = await Context.Sessions.BuildAsync<WebDriverSession>();

        // Navigate to "Products" page in scope of the first session.
        var productsPage = session1.Go.To<OrdinaryPage>(url: "products")
            .PageTitle.Should.Contain("Products"); // Verify its title.

        // Navigate to "Calculations" page in scope of the second session.
        var calculationsPage = session2.Go.To<OrdinaryPage>(url: "calculations")
            .PageTitle.Should.Contain("Calculations"); // Verify its title.

        // Assert page title of the first session page.
        productsPage.PageTitle.Should.Contain("Products");

        // Assert page title of the second session page.
        calculationsPage.PageTitle.Should.Contain("Calculations");
    }

    [Test]
    [DisableSession(typeof(WebDriverSession))] // Tells Atata not to start session automatically for this test.
    public async Task CreateTwoSessionsDuringTest_NavigateThroughGoToAfterManualSessionSwitch()
    {
        // Build a first session.
        var session1 = await Context.Sessions.BuildAsync<WebDriverSession>();

        // Build a second session.
        var session2 = await Context.Sessions.BuildAsync<WebDriverSession>();

        // Switch to the first session.
        session1.SetAsCurrent();

        // Navigate to "Products" page in scope of the first session.
        var productsPage = Go.To<OrdinaryPage>(url: "products")
            .PageTitle.Should.Contain("Products"); // Verify its title.

        // Switch to the second session.
        session2.SetAsCurrent();

        // Navigate to "Calculations" page in scope of the second session.
        var calculationsPage = Go.To<OrdinaryPage>(url: "calculations")
            .PageTitle.Should.Contain("Calculations"); // Verify its title.

        // Assert page title of the first session page.
        productsPage.PageTitle.Should.Contain("Products");

        // Assert page title of the second session page.
        calculationsPage.PageTitle.Should.Contain("Calculations");
    }
}
