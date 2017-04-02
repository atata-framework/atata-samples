using Atata;
using Atata.Bootstrap;
using _ = SampleApp.AutoTests.UserEditWindow;

namespace SampleApp.AutoTests
{
    public class UserEditWindow : BSModal<_>
    {
        [FindById]
        public GeneralTabPane General { get; private set; }

        [FindById]
        public AdditionalTabPane Additional { get; private set; }

        [Term("Save", "Create")]
        public Button<UsersPage, _> Save { get; private set; }

        public class GeneralTabPane : BSTabPane<_>
        {
            public TextInput<_> FirstName { get; private set; }

            public TextInput<_> LastName { get; private set; }

            [RandomizeStringSettings("{0}@mail.com")]
            public TextInput<_> Email { get; private set; }

            public Select<Office?, _> Office { get; private set; }

            [FindByName]
            public RadioButtonList<Gender?, _> Gender { get; private set; }
        }

        public class AdditionalTabPane : BSTabPane<_>
        {
            public DateInput<_> Birthday { get; private set; }

            public TextArea<_> Notes { get; private set; }
        }
    }
}