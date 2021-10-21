using Atata;
using NUnit.Framework;

namespace AtataSamples.DownloadFile
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            AtataContext.GlobalConfiguration
                .UseChrome()
                    .WithArguments("start-maximized")
                    .WithArtifactsAsDownloadDirectory()
                .UseBaseUrl("https://atata.io/")
                .UseCulture("en-US")
                .UseAllNUnitFeatures();

            AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
        }
    }
}
