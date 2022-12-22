using Atata;
using AtataSamples.SpecFlow.Components;
using TechTalk.SpecFlow;

namespace AtataSamples.SpecFlow.StepDefinitions;

[Binding]
public sealed class PlansSteps : BaseSteps
{
    [Then(@"I verify Plans page")]
    public static void ThenIVerifyPlansPage() =>
        On<PlansPage>().PlanItems.Count.Should.Equal(3);
}
