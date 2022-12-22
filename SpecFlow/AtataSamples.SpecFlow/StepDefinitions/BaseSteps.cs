using Atata;
using TechTalk.SpecFlow;

namespace AtataSamples.SpecFlow.StepDefinitions;

[Binding]
public abstract class BaseSteps : Steps
{
    protected static TPageObject On<TPageObject>()
        where TPageObject : PageObject<TPageObject>
        =>
        (AtataContext.Current.PageObject as TPageObject)
            ?? Go.To<TPageObject>(navigate: false);
}
