namespace AtataSamples.CsvDataSource;

public sealed class UserTests : AtataTestSuite
{
    public static IEnumerable<TestCaseData> UserModels =>
        CsvSource.Get<UserModel>("user-models.csv");

    [TestCaseSource(nameof(UserModels))]
    public void Create(UserModel model) =>
        Login()
            .New.ClickAndGo()
                .General.FirstName.Set(model.FirstName)
                .General.LastName.Set(model.LastName)
                .General.Email.Set(model.Email)
                .General.Office.Set(model.Office)
                .General.Gender.Set(model.Gender)
                .Create.ClickAndGo()
            .Users.Rows.Should.Contain(row =>
                row.FirstName == model.FirstName &&
                row.LastName == model.LastName &&
                row.Email == model.Email &&
                row.Office == model.Office);

    private static UsersPage Login() =>
        Go.To<SignInPage>()
            .Email.Set("admin@mail.com")
            .Password.Set("abc123")
            .SignIn.ClickAndGo();
}
