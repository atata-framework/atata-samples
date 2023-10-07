using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;

namespace Atata.ExtentReports;

public sealed class ExtentArtifactAddedEventHandler : IEventHandler<ArtifactAddedEvent>
{
    private static readonly object s_mediaProviderSyncRoot = new();

    public void Handle(ArtifactAddedEvent eventData, AtataContext context)
    {
        if (eventData.ArtifactType == ArtifactTypes.Screenshot)
        {
            string relativeFilePath = eventData.AbsoluteFilePath.Replace(ExtentContext.WorkingDirectoryPath, null);

            ExtentContext.ResolveFor(context).Test.Log(
                Status.Info,
                CreateMediaEntityModelProvider(relativeFilePath, eventData.ArtifactTitle));
        }
    }

    private static Media CreateMediaEntityModelProvider(string relativeFilePath, string title)
    {
        // Locking is added because of a thread synchronization issue in ExtentReports in the method below.
        lock (s_mediaProviderSyncRoot)
            return MediaEntityBuilder.CreateScreenCaptureFromPath(relativeFilePath, title).Build();
    }
}
