namespace SampleApp.UITests;

public class UserTests : UITestFixture
{
    [Test]
    public void Create() =>
        Login() // Returns UsersPage.
            .New.ClickAndGo() // Returns UserEditWindow.
                .ModalTitle.Should.Equal("New User")
                .General.FirstName.SetRandom(out string firstName)
                .General.LastName.SetRandom(out string lastName)
                .General.Email.SetRandom(out string email)
                .General.Office.SetRandom(out Office office)
                .General.Gender.SetRandom(out Gender gender)
                .Save.ClickAndGo() // Returns UsersPage.
            .Users.Rows[x => x.Email == email].View.ClickAndGo() // Returns UserDetailsPage.
                .AggregateAssert(page => page
                    .Header.Should.Equal($"{firstName} {lastName}")
                    .Email.Should.Equal(email)
                    .Office.Should.Equal(office)
                    .Gender.Should.Equal(gender)
                    .Birthday.Should.Not.BePresent()
                    .Notes.Should.Not.BePresent());
}
