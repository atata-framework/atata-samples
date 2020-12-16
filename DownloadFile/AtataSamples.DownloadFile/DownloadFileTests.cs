using Atata;
using NUnit.Framework;

namespace AtataSamples.DownloadFile
{
    public class DownloadFileTests : UITestFixture
    {
        [Test]
        public void DownloadFile()
        {
            Go.To<SamplePage>(url: "/SampleApp.UITests")
                .DownloadSources.Click();

            TestDownloads.ShouldContain("SampleApp.UITests.zip", secondsToWait: 20);
        }
    }
}
