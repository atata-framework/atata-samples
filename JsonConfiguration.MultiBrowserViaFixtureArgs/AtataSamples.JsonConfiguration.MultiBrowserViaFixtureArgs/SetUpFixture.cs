namespace AtataSamples.JsonConfiguration.MultiBrowserViaFixtureArguments;

[SetUpFixture]
public sealed class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp() =>
        AtataContext.GlobalConfiguration
            .ApplyJsonConfig()
            .AutoSetUpConfiguredDrivers();
}
