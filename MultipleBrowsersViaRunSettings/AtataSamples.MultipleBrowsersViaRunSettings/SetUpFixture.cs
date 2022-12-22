using Atata;
using Atata.WebDriverSetup;
using NUnit.Framework;

namespace AtataSamples.MultipleBrowsersViaRunSettings;

[SetUpFixture]
public class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        string driverAlias = TestContext.Parameters.Get("DriverAlias", DriverAliases.Chrome);

        AtataContext.GlobalConfiguration
            .ApplyJsonConfig()
            .UseDriver(driverAlias);

        DriverSetup.GetDefaultConfiguration(BrowserNames.InternetExplorer)
            .WithX32Architecture();

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }
}
