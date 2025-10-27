namespace AtataSamples.Configuration.Mixed;

public sealed class SampleTests : AtataTestSuite
{
    [Test]
    public void SampleTest()
    {
        var config = Context.State.Get<GlobalConfig>();
        var username = config.Username;
        var password = config.Password;
        Context.Log.Info($"Username: {username}; Password: {password}");

        string atataVariableValue = (string)Context.Variables["key"]!;
        Context.Log.Info($"key: {atataVariableValue}");

        Go.To<OrdinaryPage>()
            .PageTitle.Should.Contain("Atata");
    }
}
