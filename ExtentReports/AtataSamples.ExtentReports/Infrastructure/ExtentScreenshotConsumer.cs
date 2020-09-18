using System.IO;
using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace Atata.ExtentReports
{
    public class ExtentScreenshotConsumer : IScreenshotConsumer
    {
        public void Take(ScreenshotInfo screenshotInfo)
        {
            string title = $"{screenshotInfo.Number:D2} - {screenshotInfo.PageObjectFullName}{screenshotInfo.Title?.Prepend(" - ")}";

            string relativeFilePath = Path.Combine(AtataContext.Current.TestNameSanitized, $"{title.SanitizeForFileName()}.png");
            string absoluteFilePath = Path.Combine(ExtentContext.WorkingFolder, relativeFilePath);

            string targetDirectory = Path.GetDirectoryName(absoluteFilePath);

            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);

            screenshotInfo.Screenshot.SaveAsFile(absoluteFilePath, ScreenshotImageFormat.Png);

            ExtentContext.CurrentTest.Log(
                Status.Info,
                MediaEntityBuilder.CreateScreenCaptureFromPath(relativeFilePath, title).Build());
        }
    }
}
