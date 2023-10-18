using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Atata;
using Atata.ExtentReports;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;

namespace AtataSamples.ExtentReports;

public sealed class AddArtifactsToExtentReportEventHandler : IEventHandler<AtataContextDeInitCompletedEvent>
{
    public void Handle(AtataContextDeInitCompletedEvent eventData, AtataContext context)
    {
        DirectoryInfo directory = context.Artifacts.Object;

        if (directory.Exists)
        {
            var relativeFilePaths = directory.EnumerateFiles("*", SearchOption.AllDirectories)
                .OrderBy(x => x.CreationTimeUtc)
                .Select(x => x.FullName.TrimStart(ExtentContext.WorkingDirectoryPath));

            IMarkup markup = new ArtifactsListMarkup(relativeFilePaths);

            ExtentContext.ResolveFor(context).Test.Log(Status.Info, markup);
        }
    }

    private sealed class ArtifactsListMarkup : IMarkup
    {
        private readonly IEnumerable<string> _relativeFilePaths;

        public ArtifactsListMarkup(IEnumerable<string> relativeFilePaths) =>
            _relativeFilePaths = relativeFilePaths;

        public string GetMarkup()
        {
            StringBuilder builder = new StringBuilder("<span>Artifacts:</span><ul>");

            foreach (string relativeFilePath in _relativeFilePaths)
                builder.Append($"<li><a href=\"{relativeFilePath}\">{Path.GetFileName(relativeFilePath)}</a></li>");

            builder.Append("</ul>");
            return builder.ToString();
        }
    }
}
