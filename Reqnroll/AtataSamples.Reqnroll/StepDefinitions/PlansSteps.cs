namespace AtataSamples.Reqnroll.StepDefinitions;

[Binding]
public sealed class PlansSteps : Steps
{
    [Then(@"I verify Plans page")]
    public static void ThenIVerifyPlansPage() =>
        Go.On<PlansPage>().PlanItems.Count.Should.Equal(3);
}
