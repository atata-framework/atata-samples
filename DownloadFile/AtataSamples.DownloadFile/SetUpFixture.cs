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
                    .WithLocalDriverPath()
                .OnBuilt(TestDownloads.DeleteDirectory)
                .OnCleanUp(TestDownloads.DeleteDirectory)
                .UseBaseUrl("https://github.com/atata-framework/atata-samples/tree/master/")
                .UseCulture("en-US")
                .UseAllNUnitFeatures();
        }
    }
}
