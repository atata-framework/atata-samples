using Atata;
using NUnit.Framework;

namespace SampleApp.AutoTests
{
    public class UserTest : AutoTest
    {
        [Test]
        public void User_Create()
        {
            string firstName, lastName, email;
            Office office = Office.NewYork;
            Gender gender = Gender.Male;

            Login().
                New.ClickAndGo().
                    ModalTitle.Should.Equal("New User").
                    General.FirstName.SetRandom(out firstName).
                    General.LastName.SetRandom(out lastName).
                    General.Email.SetRandom(out email).
                    General.Office.Set(office).
                    General.Gender.Set(gender).
                    Save.ClickAndGo().
                Users.Rows[x => x.FirstName == firstName && x.LastName == lastName].View.ClickAndGo().
                    Header.Should.Equal($"{firstName} {lastName}").
                    Email.Should.Equal(email).
                    Office.Should.Equal(office).
                    Gender.Should.Equal(gender).
                    Birthday.Should.Not.Exist().
                    Notes.Should.Not.Exist();
        }
    }
}