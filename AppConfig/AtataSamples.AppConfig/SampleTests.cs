using Atata;
using NUnit.Framework;

namespace AtataSamples.AppConfig
{
    public class SampleTests : UITestFixture
    {
        [Test]
        public void SignIn() =>
            Go.To<SignInPage>()
                .Email.Set(Config.Account.Email)
                .Password.Set(Config.Account.Password)
                .SignIn.ClickAndGo()
                    .Heading.Should.Equal("Users"); // Verify that we are navigated to "Users" page.
    }
}
