namespace AtataSamples.JsonConfiguration.MultiEnvironment;

public class SignInTests : UITestFixture
{
    [Test]
    public void SignIn() =>
        Go.To<HomePage>()
            .SignIn.ClickAndGo()
                .Email.Set(AtataConfig.Current.AccountEmail)
                .Password.Set(AtataConfig.Current.AccountPassword);
}
