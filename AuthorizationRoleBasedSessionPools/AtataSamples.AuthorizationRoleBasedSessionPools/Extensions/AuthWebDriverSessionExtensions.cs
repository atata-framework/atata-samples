namespace AtataSamples.AuthorizationRoleBasedSessionPools;

public static class AuthWebDriverSessionExtensions
{
    public static void Login(this WebDriverSession session, UserCredentials credentials) =>
        session.Go.To<SignInPage>()
            .Email.Set(credentials.Email)
            .Password.Set(credentials.Password)
            .SignIn.Click();
}
