using Atata;

namespace AtataSamples.PageVerification
{
    using _ = PlansWithStaticTriggersPage;

    [Url("plans")]
    [VerifyTitle("Plans - Atata Sample App")]
    [VerifyH1("Plans")]
    [VerifyContent("Please choose your payment plan")]
    public class PlansWithStaticTriggersPage : Page<_>
    {
    }
}
