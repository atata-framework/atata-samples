using Atata;
using NUnit.Framework;

namespace AtataSamples.CsvDataSource
{
    public class UserTests : UITestFixture
    {
        public static TestCaseData[] UserModels =>
            CsvSource.Get<UserModel>("user-models.csv");

        [TestCaseSource(nameof(UserModels))]
        public void User_Create_New(UserModel model)
        {
            Login().
                New.ClickAndGo().
                    General.FirstName.Set(model.FirstName).
                    General.LastName.Set(model.LastName).
                    General.Email.Set(model.Email).
                    General.Office.Set(model.Office).
                    General.Gender.Set(model.Gender).
                    Create.ClickAndGo().
                Users.Rows.Should.Contain(row =>
                    row.FirstName == model.FirstName &&
                    row.LastName == model.LastName &&
                    row.Email == model.Email &&
                    row.Office == model.Office);
        }
    }
}
