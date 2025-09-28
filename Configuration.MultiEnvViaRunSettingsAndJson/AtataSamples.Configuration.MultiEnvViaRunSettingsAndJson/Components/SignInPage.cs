namespace AtataSamples.Configuration.MultiEnvViaRunSettingsAndJson;

using _ = SignInPage;

public sealed class SignInPage : Page<_>
{
    public TextInput<_> Email { get; private set; }

    public PasswordInput<_> Password { get; private set; }
}
