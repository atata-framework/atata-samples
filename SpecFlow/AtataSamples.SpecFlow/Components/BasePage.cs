using Atata;

namespace AtataSamples.Specflow.Components
{
    public abstract class BasePage<TOwner> : Page<TOwner>
        where TOwner : BasePage<TOwner>
    {

        [VerifyExists]
        [FindByContent("Calculations")]
        public Link<CalculationsPage, TOwner> Calculations { get; private set; }


        [VerifyExists]
        [FindByContent("Plans")]
        public Link<PlansPage, TOwner> Plans { get; private set; }
    }
}
