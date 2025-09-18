namespace SampleApp.UITests;

using _ = SignInPage;

[Url("signin")]
[VerifyTitle]
[VerifyH1]
public sealed class SignInPage : Page<_>
{
    public TextInput<_> Email { get; private set; }

    public PasswordInput<_> Password { get; private set; }

    public Button<UsersPage, _> SignIn { get; private set; }
}
