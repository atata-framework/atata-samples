using Atata;
using _ = AtataSamples.PageVerification.PlansPage;

namespace AtataSamples.PageVerification
{
    [Url("plans")]
    public class PlansPage : Page<_>
    {
        public H1<_> Header { get; private set; }

        public ControlList<PlanItem, _> PlanItems { get; private set; }

        [ControlDefinition("div", ContainingClass = "plan-item", ComponentTypeName = "plan item")]
        public class PlanItem : Control<_>
        {
            public H3<_> Title { get; private set; }

            [FindByClass]
            public Currency<_> Price { get; private set; }

            [FindByClass("projects-num")]
            public Number<_> NumberOfProjects { get; private set; }

            public ControlList<FeatureItem, _> Features { get; private set; }

            [ControlDefinition("li", ComponentTypeName = "feature")]
            public class FeatureItem : Text<_>
            {
            }
        }
    }
}
