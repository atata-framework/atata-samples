namespace AtataSamples.JsonConfiguration.MultiBrowserViaFixtureArguments;

[TestFixture(WebDriverAliases.Chrome)]
[TestFixture(WebDriverAliases.Edge)]
////[TestFixture(WebDriverAliases.Firefox)]
////[TestFixture("chrome-remote")]
[Parallelizable]
public abstract class TestSuite : AtataTestSuite
{
    private readonly string _driverAlias;

    protected TestSuite(string driverAlias) =>
        _driverAlias = driverAlias;

    protected override void ConfigureTestAtataContext(AtataContextBuilder builder) =>
        builder.Sessions.ConfigureWebDriver(x => x
            .UseDriver(_driverAlias));
}
