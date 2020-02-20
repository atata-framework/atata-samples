using Atata;
using NUnit.Framework;

namespace AtataSamples.NUnit.GenericPageTests
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            AtataContext.GlobalConfiguration.
                UseChrome().
                    WithArguments("start-maximized").
                    WithLocalDriverPath().
                UseBaseUrl("https://demo.atata.io/").
                UseCulture("en-us").
                UseAllNUnitFeatures();
        }
    }
}
