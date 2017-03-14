using Atata;
using _ = AtataSamples.PageVerification.Components.PlansPage;

namespace AtataSamples.PageVerification.Components
{
    [Url("plans")]
    public class PlansPage : Page<_>
    {
        public H1<_> Header { get; private set; }
    }
}
