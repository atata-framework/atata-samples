namespace AtataSamples.Xunit;

public sealed class SampleTests : AtataTestSuite
{
    [Fact]
    public void SampleTest() =>
        Go.To<OrdinaryPage>()
           .PageTitle.Should.Contain("Atata");
}
