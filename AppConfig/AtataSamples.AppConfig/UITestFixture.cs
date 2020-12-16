using Atata;
using NUnit.Framework;

namespace AtataSamples.AppConfig
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
                UseBaseUrl(Config.BaseUrl).
                UseCulture("en-US").
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
