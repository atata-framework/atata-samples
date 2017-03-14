using Atata;
using AtataSamples.PageVerification.Components;
using NUnit.Framework;

namespace AtataSamples.PageVerification
{
    [TestFixture]
    public class PlanTests
    {
        [SetUp]
        public void SetUp()
        {
            AtataContext.Build().
                UseChrome().
                    WithArguments("disable-extensions", "no-sandbox", "start-maximized").
                UseBaseUrl("https://atata-framework.github.io/atata-sample-app/#!/").
                UseNUnitTestName().
                AddNUnitTestContextLogging().
                    WithMinLevel(LogLevel.Info).
                    WithoutSectionFinish().
                LogNUnitError().
                SetUp();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current.CleanUp();
        }

        [Test]
        public void PrimaryPageDataVerification_InTest()
        {
            Go.To<PlansPage>().
                PageTitle.Should.Equal("Plans - Atata Sample App").
                Header.Should.Equal("Plans").
                Content.Should.Contain("Please choose your payment plan");
        }

        [Test]
        public void PrimaryPageDataVerification_StaticTriggers()
        {
            Go.To<PlansWithStaticTriggersPage>();
        }

        [Test]
        public void PrimaryPageDataVerification_DynamicTriggers()
        {
            Go.To<PlansWithDynamicTriggersPage>();
        }
    }
}
