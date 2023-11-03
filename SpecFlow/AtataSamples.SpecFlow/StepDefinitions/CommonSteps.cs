using Atata;
using AtataSamples.SpecFlow.Components;
using TechTalk.SpecFlow;

namespace AtataSamples.SpecFlow.StepDefinitions;

[Binding]
public sealed class CommonSteps : Steps
{
    [Given(@"I am on Home Page")]
    public static void GivenIAmOnHomePage() =>
        Go.To<HomePage>();

    [When(@"I navigate to Calculations page by header's button")]
    public static void WhenINavigateToCalculationsPageByHeaderSButton() =>
        Go.On<HomePage>().Calculations.ClickAndGo();

    [When(@"I navigate to Plans page by header's button")]
    public static void WhenINavigateToPlansPageByHeaderSButton() =>
        Go.On<HomePage>().Plans.ClickAndGo();
}
