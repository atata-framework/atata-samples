using Atata;
using NUnit.Framework;

namespace SampleApp.UITests
{
    public class UserTests : UITestFixture
    {
        [Test]
        public void Create()
        {
            string firstName, lastName, email;
            Office office = Office.NewYork;
            Gender gender = Gender.Male;

            Login(). // Returns UsersPage.
                New.ClickAndGo(). // Returns UserEditWindow.
                    ModalTitle.Should.Equal("New User").
                    General.FirstName.SetRandom(out firstName).
                    General.LastName.SetRandom(out lastName).
                    General.Email.SetRandom(out email).
                    General.Office.Set(office).
                    General.Gender.Set(gender).
                    Save.ClickAndGo(). // Returns UsersPage.
                Users.Rows[x => x.FirstName == firstName && x.LastName == lastName].View.ClickAndGo(). // Returns UserDetailsPage.
                    AggregateAssert(x => x.
                        Header.Should.Equal($"{firstName} {lastName}").
                        Email.Should.Equal(email).
                        Office.Should.Equal(office).
                        Gender.Should.Equal(gender).
                        Birthday.Should.Not.Exist().
                        Notes.Should.Not.Exist());
        }
    }
}
