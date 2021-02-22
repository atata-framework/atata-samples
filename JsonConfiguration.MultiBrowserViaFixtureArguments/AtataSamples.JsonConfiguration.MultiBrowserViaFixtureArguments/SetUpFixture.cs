using Atata;
using Atata.WebDriverSetup;
using NUnit.Framework;

namespace AtataSamples.JsonConfiguration.MultiBrowserViaFixtureArguments
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            AtataContext.GlobalConfiguration.ApplyJsonConfig();

            DriverSetup.GetDefaultConfiguration(BrowserNames.InternetExplorer)
                .WithX32Architecture();

            AtataContext.GlobalConfiguration.AutoSetUpConfiguredDrivers();
        }
    }
}
