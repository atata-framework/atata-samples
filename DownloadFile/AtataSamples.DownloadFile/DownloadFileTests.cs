namespace AtataSamples.DownloadFile;

public sealed class DownloadFileTests : AtataTestSuite
{
    [Test]
    public void DownloadFile()
    {
        Go.To<TutorialPage>(url: "/tutorials/verification-of-page/")
            .DownloadSources.Click();

        Context.Artifacts.Should.WithinSeconds(10).ContainFile("PageVerification.zip");
    }
}
