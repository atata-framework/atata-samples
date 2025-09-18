namespace AtataSamples.JsonConfiguration.MultiBrowserViaFixtureArguments;

[SetUpFixture]
public class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp() =>
        AtataContext.GlobalConfiguration
            .ApplyJsonConfig()
            .AutoSetUpConfiguredDrivers();
}
