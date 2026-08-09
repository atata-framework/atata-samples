namespace AtataSamples.AspNetCore.UITests;

public sealed class HomeTests : AtataTestSuite
{
    [Test]
    public void Home() =>
        Go.To<HomePage>()
            .Header.Should.Be("Welcome");
}
