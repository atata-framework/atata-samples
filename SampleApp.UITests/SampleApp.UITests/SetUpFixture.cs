using Atata;
using NUnit.Framework;

namespace SampleApp.UITests;

[SetUpFixture]
public class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        AtataContext.GlobalConfiguration
            .UseChrome()
                .WithArguments("start-maximized")
            .UseBaseUrl("https://demo.atata.io/")
            .UseCulture("en-US")
            .UseAllNUnitFeatures()
            .Attributes.Global.Add(
                new VerifyTitleSettingsAttribute { Format = "{0} - Atata Sample App" });

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }
}
