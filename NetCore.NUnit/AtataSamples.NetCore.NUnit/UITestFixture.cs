using Atata;
using NUnit.Framework;

namespace AtataSamples.NetCore.NUnit
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
                    WithFixOfCommandExecutionDelay().
                    WithLocalDriverPath().
                UseBaseUrl("https://atata-framework.github.io/atata-sample-app/#!/").
                UseCulture("en-us").
                UseNUnitTestName().
                AddNUnitTestContextLogging().
                LogNUnitError().
                Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }
    }
}
