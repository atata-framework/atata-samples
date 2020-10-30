using Atata;

namespace AtataSamples.PageVerification
{
    using _ = PlansWithDynamicTriggersPage;

    [Url("plans")]
    public class PlansWithDynamicTriggersPage : Page<_>
    {
        public PlansWithDynamicTriggersPage()
        {
            Metadata.Add(
                new VerifyTitleAttribute("Plans - Atata Sample App"),
                new VerifyH1Attribute("Plans"),
                new VerifyContentAttribute("Please choose your payment plan"));
        }
    }
}
