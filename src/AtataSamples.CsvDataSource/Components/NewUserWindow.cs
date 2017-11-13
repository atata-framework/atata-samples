using Atata;
using Atata.Bootstrap;

namespace AtataSamples.CsvDataSource
{
    using _ = NewUserWindow;

    public class NewUserWindow : BSModal<_>
    {
        [FindById]
        public GeneralTabPane General { get; private set; }

        public Button<UsersPage, _> Create { get; private set; }

        public class GeneralTabPane : BSTabPane<_>
        {
            public TextInput<_> FirstName { get; private set; }

            public TextInput<_> LastName { get; private set; }

            public TextInput<_> Email { get; private set; }

            public Select<Office?, _> Office { get; private set; }

            [FindByName]
            public RadioButtonList<Gender?, _> Gender { get; private set; }
        }
    }
}
