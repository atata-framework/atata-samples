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
            AtataContext.Configure().
                UseChrome().
                    WithArguments("start-maximized").
                UseBaseUrl("https://atata-framework.github.io/atata-sample-app/#!/").
                UseNUnitTestName().
                AddNUnitTestContextLogging().
                    WithoutSectionFinish().
                LogNUnitError().
                Build();
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
