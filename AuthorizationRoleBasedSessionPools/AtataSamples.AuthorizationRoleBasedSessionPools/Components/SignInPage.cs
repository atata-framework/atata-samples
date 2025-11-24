namespace AtataSamples.AuthorizationRoleBasedSessionPools;

using _ = SignInPage;

[Url("/signin")]
public sealed class SignInPage : Page<_>
{
    public TextInput<_> Email { get; private set; }

    public PasswordInput<_> Password { get; private set; }

    public Button<_> SignIn { get; private set; }
}
