using Atata;
using NUnit.Framework;

namespace SampleApp.AutoTests
{
    [TestFixture]
    public class UITestFixture
    {
        [SetUp]
        public void SetUp()
        {
            AtataContext.Build().
                UseChrome().
                    WithArguments("disable-extensions", "no-sandbox", "start-maximized").
                UseBaseUrl("http://atata-framework.github.io/atata-sample-app/#!/").
                UseNUnitTestName().
                AddNUnitTestContextLogging().
                    WithoutSectionFinish().
                LogNUnitError().
                SetUp();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current.CleanUp();
        }

        protected UsersPage Login()
        {
            return Go.To<SignInPage>().
                Email.Set("admin@mail.com").
                Password.Set("abc123").
                SignIn.ClickAndGo();
        }
    }
}