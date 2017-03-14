using Atata;
using _ = AtataSamples.PageVerification.Components.PlansWithStaticTriggersPage;

namespace AtataSamples.PageVerification.Components
{
    [Url("plans")]
    [VerifyTitle("Plans - Atata Sample App")]
    [VerifyH1("Plans")]
    [VerifyContent("Please choose your payment plan")]
    public class PlansWithStaticTriggersPage : Page<_>
    {
    }
}
