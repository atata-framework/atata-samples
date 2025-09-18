namespace AtataSamples.CsvDataSource;

[Parallelizable(ParallelScope.Self)]
public abstract class UITestFixture
{
    [SetUp]
    public void SetUp() =>
        AtataContext.Configure().Build();

    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.Dispose();

    protected static UsersPage Login() =>
        Go.To<SignInPage>()
            .Email.Set("admin@mail.com")
            .Password.Set("abc123")
            .SignIn.ClickAndGo();
}
