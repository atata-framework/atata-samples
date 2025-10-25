namespace AtataSamples.Xunit;

[Collection(SomeCollection.Name)]
public sealed class WithCollectionFixtureTests : AtataTestSuite
{
    [Fact]
    [DisableSession(typeof(WebDriverSession))]
    public void Context_Variables() =>
        Context.Variables.ToSutSubject().ValueOf(x => x[nameof(SomeCollectionFixture)]).Should.Be(true);
}
