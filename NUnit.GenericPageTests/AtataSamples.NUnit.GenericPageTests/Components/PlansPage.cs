using Atata;

namespace AtataSamples.NUnit.GenericPageTests
{
    using _ = PlansPage;

    [Url("plans")]
    [VerifyH1]
    public class PlansPage : AppPage<_>, IPageWithHeader<_>
    {
        public H1<_> Header { get; private set; }
    }
}
