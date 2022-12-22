using Atata;
using NUnit.Framework;

namespace AtataSamples.DownloadFile;

public class DownloadFileTests : UITestFixture
{
    [Test]
    public void DownloadFile()
    {
        Go.To<TutorialPage>(url: "/tutorials/verification-of-page/")
            .DownloadSources.Click();

        AtataContext.Current.Artifacts.Should.WithinSeconds(10).ContainFile("PageVerification.zip");
    }
}
