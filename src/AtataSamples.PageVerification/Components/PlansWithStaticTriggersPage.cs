using Atata;
using _ = AtataSamples.PageVerification.PlansWithStaticTriggersPage;

namespace AtataSamples.PageVerification
{
    [Url("plans")]
    [VerifyTitle("Plans - Atata Sample App")]
    [VerifyH1("Plans")]
    [VerifyContent("Please choose your payment plan")]
    public class PlansWithStaticTriggersPage : Page<_>
    {
    }
}
