using Atata;
using NUnit.Framework;

namespace SampleApp.UITests
{
    public class UserTests : UITestFixture
    {
        [Test]
        public void Create()
        {
            Office office = Office.NewYork;
            Gender gender = Gender.Male;

            Login() // Returns UsersPage.
                .New.ClickAndGo() // Returns UserEditWindow.
                    .ModalTitle.Should.Equal("New User")
                    .General.FirstName.SetRandom(out string firstName)
                    .General.LastName.SetRandom(out string lastName)
                    .General.Email.SetRandom(out string email)
                    .General.Office.Set(office)
                    .General.Gender.Set(gender)
                    .Save.ClickAndGo() // Returns UsersPage.
                .Users.Rows[x => x.Email == email].View.ClickAndGo() // Returns UserDetailsPage.
                    .AggregateAssert(page => page
                        .Header.Should.Equal($"{firstName} {lastName}")
                        .Email.Should.Equal(email)
                        .Office.Should.Equal(office)
                        .Gender.Should.Equal(gender)
                        .Birthday.Should.Not.Exist()
                        .Notes.Should.Not.Exist());
        }
    }
}
