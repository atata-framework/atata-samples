namespace AtataSamples.SpecFlow.Components;

public abstract class BasePage<TOwner> : Page<TOwner>
    where TOwner : BasePage<TOwner>
{
    [FindByContent("Calculations")]
    public Link<CalculationsPage, TOwner> Calculations { get; private set; }

    [FindByContent("Plans")]
    public Link<PlansPage, TOwner> Plans { get; private set; }
}
