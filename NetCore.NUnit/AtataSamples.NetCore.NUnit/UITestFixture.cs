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
                    WithLocalDriverPath().
                UseBaseUrl("https://demo.atata.io/").
                UseCulture("en-us").
                UseAllNUnitFeatures().
                Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }
    }
}
