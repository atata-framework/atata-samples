namespace AtataSamples.Xunit;

[SetVariable("class-attribute-variable", true)] // Serves test purpose.
public sealed class WithClassFixtureTests :
    AtataTestSuite,
    IClassFixture<SomeClassFixture<WithClassFixtureTests>>
{
    [Fact]
    [SetVariable("method-attribute-variable", true)] // Serves test purpose.
    [DisableSession(typeof(WebDriverSession))]
    public void Context_Variables() =>
        Context.Variables.ToSutSubject()
            .AggregateAssert(x => x
                .ValueOf(x => x[nameof(SomeClassFixture<WithClassFixtureTests>)]).Should.Be(true)
                .ValueOf(x => x["class-attribute-variable"]).Should.Be(true)
                .ValueOf(x => x["method-attribute-variable"]).Should.Be(true));
}
