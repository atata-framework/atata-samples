namespace AtataSamples.SauceLabs;

public sealed class SampleTests
{
    [Test]
    [Explicit("Before running the test, set SAUCE_USERNAME and SAUCE_ACCESS_KEY environment variables.")]
    public void SampleTest() =>
        Go.To<OrdinaryPage>()
            .PageTitle.Should.Contain("Atata");
}
