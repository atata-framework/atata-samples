using Atata;
using _ = AtataSamples.PageVerification.PlansWithDynamicTriggersPage;

namespace AtataSamples.PageVerification
{
    [Url("plans")]
    public class PlansWithDynamicTriggersPage : Page<_>
    {
        public PlansWithDynamicTriggersPage()
        {
            Triggers.Add(
                new VerifyTitleAttribute("Plans - Atata Sample App"),
                new VerifyH1Attribute("Plans"),
                new VerifyContentAttribute("Please choose your payment plan"));
        }
    }
}
