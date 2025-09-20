namespace AtataSamples.MultipleBrowsersViaRunSettings;

public sealed class HomeTests : UITestFixture
{
    [Test]
    public void Home() =>
        Go.To<HomePage>()
            .Header.Should.Be("Atata Sample App");
}
