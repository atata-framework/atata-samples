using Atata;
using NUnit.Framework;

namespace AtataSamples.NetCore.NUnit
{
    public class SignInTests : UITestFixture
    {
        [Test]
        public void NetCoreNUnit_SignIn()
        {
            Go.To<SignInPage>().
                Email.Set("admin@mail.com").
                Password.Set("abc123").
                SignIn.ClickAndGo().
                    Heading.Should.Equal("Users"); // Verify that we are navigated to "Users" page.
        }

        [Test]
        public void NetCoreNUnit_SignIn_WithInvalidPassword()
        {
            Go.To<SignInPage>().
                Email.Set("admin@mail.com").
                Password.Set("WRONGPASSWORD").
                SignIn.Click().
                Heading.Should.Equal("Sign In"). // Verify that we are still on "Sing In" page.
                Content.Should.Contain("Password or Email is invalid"); // Verify validation message.
        }
    }
}
