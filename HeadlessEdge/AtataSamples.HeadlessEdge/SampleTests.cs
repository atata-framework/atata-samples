namespace AtataSamples.HeadlessEdge;

public sealed class SampleTests : UITestFixture
{
    [Test]
    public void Edge_Headless() =>
        Go.To<OrdinaryPage>().PageTitle.Should.Contain("Atata");
}
