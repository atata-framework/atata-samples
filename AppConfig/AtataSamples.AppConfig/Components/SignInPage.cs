using Atata;

namespace AtataSamples.AppConfig
{
    using _ = SignInPage;

    [Url("signin")]
    public class SignInPage : Page<_>
    {
        public H1<_> Heading { get; private set; }

        public TextInput<_> Email { get; private set; }

        public PasswordInput<_> Password { get; private set; }

        public Button<UsersPage, _> SignIn { get; private set; }
    }
}
