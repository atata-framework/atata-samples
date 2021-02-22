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
                    .WithOptions(x => x
                        .AddUserProfilePreference("download.default_directory", TestDownloads.DirectoryPath))
                .OnBuilt(TestDownloads.DeleteDirectory)
                .OnCleanUp(TestDownloads.DeleteDirectory)
                .UseBaseUrl("https://atata.io/")
                .UseCulture("en-US")
                .UseAllNUnitFeatures();

            AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
        }
    }
}
