namespace AtataSamples.MSTest;

[TestClass]
public sealed class SampleTests : AtataTestSuite
{
    [TestMethod]
    public void SampleTest() =>
        Go.To<OrdinaryPage>()
           .PageTitle.Should.Contain("Atata");
}
