using AventStack.ExtentReports;

namespace Atata.ExtentReports;

public class ExtentScreenshotFileEventHandler : IEventHandler<ScreenshotFileSavedEvent>
{
    private static readonly object s_mediaProviderSyncRoot = new();

    public void Handle(ScreenshotFileSavedEvent eventData, AtataContext context)
    {
        string relativeFilePath = eventData.FilePath.Replace(ExtentContext.WorkingDirectoryPath, null);

        ExtentContext.ResolveFor(context).Test.Log(
            Status.Info,
            CreateMediaEntityModelProvider(relativeFilePath, eventData.ScreenshotInfo.Title));
    }

    private static MediaEntityModelProvider CreateMediaEntityModelProvider(string relativeFilePath, string title)
    {
        // Locking is added because of a thread synchronization issue in ExtentReports in methods below.
        lock (s_mediaProviderSyncRoot)
            return MediaEntityBuilder.CreateScreenCaptureFromPath(relativeFilePath, title).Build();
    }
}
