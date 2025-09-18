namespace AtataSamples.JsonExtendedConfiguration;

public class SampleTests : UITestFixture
{
    [Test]
    public void SampleTest()
    {
        var username = AtataConfig.Current.Username;
        var password = AtataConfig.Current.Password;
        AtataContext.Current.Log.Info($"Username: {username}; Password: {password}");

        string atataVariableValue = (string)AtataContext.Current.Variables["key"];
        AtataContext.Current.Log.Info($"key: {atataVariableValue}");

        Go.To<OrdinaryPage>()
            .PageTitle.Should.Contain("Atata");
    }
}
