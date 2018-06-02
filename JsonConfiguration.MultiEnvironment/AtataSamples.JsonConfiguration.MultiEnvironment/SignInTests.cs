using Atata;
using NUnit.Framework;

namespace AtataSamples.JsonConfiguration.MultiEnvironment
{
    public class SignInTests : UITestFixture
    {
        [Test]
        public void SignIn()
        {
            Go.To<HomePage>().
                SignIn.ClickAndGo().
                    Email.Set(AppConfig.Current.AccountEmail).
                    Password.Set(AppConfig.Current.AccountPassword);
        }
    }
}
