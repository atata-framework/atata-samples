using Atata;
using Atata.WebDriverSetup;
using NUnit.Framework;
using OpenQA.Selenium.Edge;

namespace AtataSamples.HeadlessEdge
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            AtataContext.GlobalConfiguration
                .UseDriver(() =>
                {
                    EdgeOptions options = new EdgeOptions
                    {
                        UseChromium = true
                    };

                    options.AddArguments("headless", "disable-gpu", "window-size=1024,768");

                    return new EdgeDriver(options);
                })
                .UseBaseUrl("https://demo.atata.io/")
                .UseCulture("en-US")
                .UseAllNUnitFeatures();

            DriverSetup.AutoSetUp(BrowserNames.Edge);
        }
    }
}
