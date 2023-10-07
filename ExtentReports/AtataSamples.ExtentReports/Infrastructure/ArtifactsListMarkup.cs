using System.Collections.Generic;
using System.IO;
using System.Text;
using AventStack.ExtentReports.MarkupUtils;

namespace AtataSamples.ExtentReports;

public sealed class ArtifactsListMarkup : IMarkup
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
