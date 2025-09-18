namespace AtataSamples.DownloadFile;

using _ = TutorialPage;

public sealed class TutorialPage : Page<_>
{
    [Term("Download sources")]
    public Link<_> DownloadSources { get; private set; }
}
