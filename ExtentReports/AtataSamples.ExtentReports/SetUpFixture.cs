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
                .UseBaseUrl("https://demo.atata.io/")
                .UseCulture("en-US")
                .UseAllNUnitFeatures()
                .AddScreenshotFileSaving()
                    .WithArtifactsFolderPath()
                .EventSubscriptions.Add(new ExtentScreenshotFileEventHandler());

            AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
        }

        [OneTimeTearDown]
        public void GlobalTearDown() =>
            ExtentContext.Reports.Flush();
    }
}
