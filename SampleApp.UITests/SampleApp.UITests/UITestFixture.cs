using Atata;
using NUnit.Framework;

namespace SampleApp.UITests
{
    [TestFixture]
    public class UITestFixture
    {
        [SetUp]
        public void SetUp()
        {
            AtataContext.Configure().Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }

        protected static UsersPage Login()
        {
            return Go.To<SignInPage>()
                .Email.Set("admin@mail.com")
                .Password.Set("abc123")
                .SignIn.ClickAndGo();
        }
    }
}
