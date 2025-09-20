namespace AtataSamples.ValidationMessagesVerification;

public sealed class SignUpTests : UITestFixture
{
    [Test]
    public void Validation_Required() =>
        Go.To<SignUpPage>()
            .SignUp.Click()
            .AggregateAssert(page => page
                .ValidationMessages[x => x.FirstName].Should.Be("is required")
                .ValidationMessages[x => x.LastName].Should.Be("is required")
                .ValidationMessages[x => x.Email].Should.Be("is required")
                .ValidationMessages[x => x.Password].Should.Be("is required")
                .ValidationMessages[x => x.Agreement].Should.Be("is required")
                .ValidationMessages.Should.HaveCount(5));

    [Test]
    public void Validation_Required_UsingExtensions() =>
        Go.To<SignUpPage>()
            .SignUp.Click()
            .AggregateAssert(page => page
                .ValidationMessages[x => x.FirstName].Should.BeRequired()
                .ValidationMessages[x => x.LastName].Should.BeRequired()
                .ValidationMessages[x => x.Email].Should.BeRequired()
                .ValidationMessages[x => x.Password].Should.BeRequired()
                .ValidationMessages[x => x.Agreement].Should.BeRequired()
                .ValidationMessages.Should.HaveCount(5));

    [Test]
    public void Validation_MinLength() =>
        Go.To<SignUpPage>()
            .FirstName.Set("a")
            .LastName.Set("a")
            .Password.Set("a")
            .SignUp.Click()
            .AggregateAssert(page => page
                .ValidationMessages[x => x.FirstName].Should.Be("minimum length is 2")
                .ValidationMessages[x => x.LastName].Should.Be("minimum length is 2")
                .ValidationMessages[x => x.Password].Should.Be("minimum length is 6"));

    [Test]
    public void Validation_MinLength_UsingExtensions() =>
        Go.To<SignUpPage>()
            .FirstName.Set("a")
            .LastName.Set("a")
            .Password.Set("a")
            .SignUp.Click()
            .AggregateAssert(page => page
                .ValidationMessages[x => x.FirstName].Should.HaveMinLength(2)
                .ValidationMessages[x => x.LastName].Should.HaveMinLength(2)
                .ValidationMessages[x => x.Password].Should.HaveMinLength(6));

    [Test]
    public void Validation_IncorrectEmail() =>
        Go.To<SignUpPage>()
            .Email.Set("some@email")
            .SignUp.Click()
            .ValidationMessages[x => x.Email].Should.Be("has incorrect format")
            .Email.Type(".com")
            .SignUp.Click()
            .ValidationMessages[x => x.Email].Should.Not.BePresent();

    [Test]
    public void Validation_IncorrectEmail_UsingExtensions() =>
        Go.To<SignUpPage>()
            .Email.Set("some@email")
            .SignUp.Click()
            .ValidationMessages[x => x.Email].Should.HaveIncorrectFormat()
            .Email.Type(".com")
            .SignUp.Click()
            .ValidationMessages[x => x.Email].Should.Not.BePresent();
}
