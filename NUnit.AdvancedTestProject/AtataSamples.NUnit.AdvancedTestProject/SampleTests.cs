namespace AtataSamples.NUnit.AdvancedTestProject;

public sealed class SampleTests : AtataTestSuite
{
    [Test]
    public void SampleTest() =>
        Go.To<OrdinaryPage>()
            .PageTitle.Should.Contain("Atata");
}
