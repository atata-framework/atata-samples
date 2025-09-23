namespace AtataSamples.HeadlessEdge;

public sealed class SampleTests : AtataTestSuite
{
    [Test]
    public void Edge_Headless() =>
        Go.To<OrdinaryPage>().PageTitle.Should.Contain("Atata");
}
