using Atata;
using Atata.ExtentReports;
using NUnit.Framework;

namespace AtataSamples.ExtentReports
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
                .UseBaseUrl("https://atata.io/")
                .UseCulture("en-US")
                .UseAllNUnitFeatures()
                // Extent Reports specific configuration:
                .AddLogConsumer(new ExtentLogConsumer())
                .AddScreenshotConsumer(new ExtentScreenshotConsumer());

            AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            ExtentContext.Reports.Flush();
        }
    }
}
