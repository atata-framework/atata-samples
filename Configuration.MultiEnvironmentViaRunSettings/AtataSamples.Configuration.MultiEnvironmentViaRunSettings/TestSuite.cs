namespace AtataSamples.Configuration.MultiEnvironmentViaRunSettings;

public abstract class TestSuite : AtataTestSuite
{
    protected GlobalConfig Config =>
        Context.State.Get<GlobalConfig>();
}
