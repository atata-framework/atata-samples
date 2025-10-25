namespace SampleApp.UITests;

public abstract class TestSuite : AtataTestSuite
{
    protected static UsersPage Login() =>
        Go.To<SignInPage>()
            .Email.Set("admin@mail.com")
            .Password.Set("abc123")
            .SignIn.ClickAndGo();
}
