namespace AtataSamples.MultipleBrowsersViaFixtureArguments;

public sealed class HomeTests : TestSuite
{
    public HomeTests(string driverAlias)
        : base(driverAlias)
    {
    }

    [Test]
    public void Home() =>
        Go.To<HomePage>()
            .Header.Should.Be("Atata Sample App");
}
