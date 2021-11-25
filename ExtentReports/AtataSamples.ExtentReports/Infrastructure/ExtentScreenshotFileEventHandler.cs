using AventStack.ExtentReports;

namespace Atata.ExtentReports
{
    public class ExtentScreenshotFileEventHandler : IEventHandler<ScreenshotFileSavedEvent>
    {
        public void Handle(ScreenshotFileSavedEvent eventData, AtataContext context)
        {
            string relativeFilePath = eventData.FilePath.Replace(ExtentContext.WorkingDirectoryPath, null);

            ExtentContext.ResolveFor(AtataContext.Current).Test.Log(
                Status.Info,
                MediaEntityBuilder.CreateScreenCaptureFromPath(relativeFilePath, eventData.ScreenshotInfo.Title).Build());
        }
    }
}
