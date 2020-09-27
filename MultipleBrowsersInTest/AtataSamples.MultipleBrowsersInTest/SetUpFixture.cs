using Atata;
using NUnit.Framework;

namespace AtataSamples.MultipleBrowsersInTest
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            AtataContext.GlobalConfiguration
                .UseChrome()
                    .WithArguments("window-size=1024,768")
                    .WithLocalDriverPath()
                .UseBaseUrl("https://demo.atata.io/")
                .UseCulture("en-US")
                .UseAllNUnitFeatures();
        }
    }
}
