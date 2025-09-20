namespace AtataSamples.JsonConfiguration.MultiBrowserViaFixtureArguments;

public sealed class HomeTests : UITestFixture
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
