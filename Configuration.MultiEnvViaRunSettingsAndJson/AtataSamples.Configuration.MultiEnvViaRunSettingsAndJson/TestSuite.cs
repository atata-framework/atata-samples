namespace AtataSamples.Configuration.MultiEnvViaRunSettingsAndJson;

public abstract class TestSuite : AtataTestSuite
{
    protected GlobalConfig Config =>
        Context.State.Get<GlobalConfig>();
}
