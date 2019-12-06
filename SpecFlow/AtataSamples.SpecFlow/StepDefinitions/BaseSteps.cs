using Atata;
using TechTalk.SpecFlow;

namespace AtataSamples.SpecFlow.StepDefinitions
{
    [Binding]
    public abstract class BaseSteps : Steps
    {
        protected TPageObject On<TPageObject>()
            where TPageObject : PageObject<TPageObject>
        {
            return (AtataContext.Current.PageObject as TPageObject)
                ?? Go.To<TPageObject>(navigate: false);
        }
    }
}
