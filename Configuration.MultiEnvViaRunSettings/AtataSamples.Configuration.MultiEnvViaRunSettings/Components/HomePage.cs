namespace AtataSamples.Configuration.MultiEnvViaRunSettings;

using _ = HomePage;

public sealed class HomePage : Page<_>
{
    [FindById]
    public Link<SignInPage, _> SignIn { get; private set; }
}
