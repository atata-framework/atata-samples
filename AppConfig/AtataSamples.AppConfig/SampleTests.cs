namespace AtataSamples.AppConfig;

public sealed class SampleTests : AtataTestSuite
{
    [Test]
    public void SignIn() =>
        Go.To<SignInPage>()
            .Email.Set(Config.Account.Email)
            .Password.Set(Config.Account.Password)
            .SignIn.ClickAndGo()
                .Heading.Should.Be("Users"); // Verify that we are navigated to "Users" page.
}
