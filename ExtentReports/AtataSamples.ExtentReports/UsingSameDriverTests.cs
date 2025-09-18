namespace AtataSamples.ExtentReports;

public sealed class UsingSameDriverTests : UITestFixture
{
    protected override bool UseFixtureDriverForTests => true;

    [OneTimeSetUp]
    public void SetUpFixture() =>
        Go.To<SignInPage>();

    [Test]
    public void Email() =>
        Go.On<SignInPage>()
            .Email.Should.BeVisible();

    [Test]
    public void Password() =>
        Go.On<SignInPage>()
            .Password.Should.BeVisible();
}
