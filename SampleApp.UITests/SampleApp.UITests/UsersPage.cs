﻿namespace SampleApp.UITests;

using _ = UsersPage;

[VerifyTitle]
[VerifyH1]
public sealed class UsersPage : Page<_>
{
    public Button<UserEditWindow, _> New { get; private set; }

    public Table<UserTableRow, _> Users { get; private set; }

    public sealed class UserTableRow : TableRow<_>
    {
        public Text<_> FirstName { get; private set; }

        public Text<_> LastName { get; private set; }

        public Text<_> Email { get; private set; }

        public Content<Office, _> Office { get; private set; }

        public Link<UserDetailsPage, _> View { get; private set; }

        public Button<UserEditWindow, _> Edit { get; private set; }

        [CloseConfirmBox]
        public Button<_> Delete { get; private set; }
    }
}
