namespace AtataSamples.Xunit;

[Collection(SomeCollection.Name)]
public sealed class WithCollectionAndClassFixturesTests :
    AtataTestSuite,
    IClassFixture<SomeClassFixture<WithCollectionAndClassFixturesTests>>
{
    [Fact]
    [DisableSession(typeof(WebDriverSession))]
    public void Context_Variables() =>
        Context.Variables.ToSubject("variables")
            .AggregateAssert(x => x
                .ValueOf(x => x[nameof(SomeCollectionFixture)]).Should.Be(true)
                .ValueOf(x => x[nameof(SomeClassFixture<WithCollectionAndClassFixturesTests>)]).Should.Be(true));
}
