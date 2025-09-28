namespace AtataSamples.Configuration.MultiEnvViaRunSettings;

public abstract class TestSuite : AtataTestSuite
{
    protected GlobalConfig Config =>
        Context.State.Get<GlobalConfig>();
}
