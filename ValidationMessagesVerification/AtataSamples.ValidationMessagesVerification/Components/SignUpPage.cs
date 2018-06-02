using Atata;

namespace AtataSamples.ValidationMessagesVerification
{
    using _ = SignUpPage;

    [Url("signup")]
    public class SignUpPage : Page<_>
    {
        public TextInput<_> FirstName { get; private set; }

        public TextInput<_> LastName { get; private set; }

        public TextInput<_> Email { get; private set; }

        public PasswordInput<_> Password { get; private set; }

        [FindByLabel("I agree to terms of service and privacy policy")]
        public CheckBox<_> Agreement { get; private set; }

        public Button<_> SignUp { get; private set; }

        public ValidationMessageList<_> ValidationMessages { get; private set; }
    }
}
