using Atata;

namespace AtataSamples.DownloadFile
{
    using _ = SamplePage;

    public class SamplePage : Page<_>
    {
        [FindByInnerXPath("img[@alt='Download sources']")]
        public Link<_> DownloadSources { get; private set; }
    }
}
