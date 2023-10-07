using System.IO;
using System.Linq;
using Atata;
using Atata.ExtentReports;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;

namespace AtataSamples.ExtentReports;

public sealed class AddArtifactsToExtentReportOnCleanUpEventHandler : IEventHandler<AtataContextCleanUpEvent>
{
    public void Handle(AtataContextCleanUpEvent eventData, AtataContext context)
    {
        DirectoryInfo directory = context.Artifacts.Object;

        if (directory.Exists)
        {
            var relativeFilePaths = directory.EnumerateFiles("*", SearchOption.AllDirectories)
                .OrderBy(x => x.CreationTimeUtc)
                .Select(x => x.FullName.Replace(ExtentContext.WorkingDirectoryPath, null));

            IMarkup markup = new ArtifactsListMarkup(relativeFilePaths);

            ExtentContext.ResolveFor(context).Test.Log(Status.Info, markup);
        }
    }
}
