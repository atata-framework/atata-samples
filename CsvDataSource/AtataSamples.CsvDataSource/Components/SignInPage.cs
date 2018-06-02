using Atata;

namespace AtataSamples.CsvDataSource
{
    using _ = SignInPage;

    [Url("signin")]
    public class SignInPage : Page<_>
    {
        public TextInput<_> Email { get; private set; }

        public PasswordInput<_> Password { get; private set; }

        public Button<UsersPage, _> SignIn { get; private set; }
    }
}
