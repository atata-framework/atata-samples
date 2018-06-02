using Atata;

namespace AtataSamples.JsonConfiguration.MultiEnvironment
{
    using _ = SignInPage;

    public class SignInPage : Page<_>
    {
        public TextInput<_> Email { get; private set; }

        public PasswordInput<_> Password { get; private set; }
    }
}
