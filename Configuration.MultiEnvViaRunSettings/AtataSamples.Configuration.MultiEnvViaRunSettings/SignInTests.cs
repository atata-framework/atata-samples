namespace AtataSamples.Configuration.MultiEnvViaRunSettings;

public sealed class SignInTests : TestSuite
{
    [Test]
    public void SignIn() =>
        Go.To<HomePage>()
            .SignIn.ClickAndGo()
                .Email.Set(Config.AccountEmail)
                .Password.Set(Config.AccountPassword);
}
