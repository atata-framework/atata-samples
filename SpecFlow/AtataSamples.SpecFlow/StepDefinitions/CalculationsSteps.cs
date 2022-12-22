using Atata;
using AtataSamples.SpecFlow.Components;
using TechTalk.SpecFlow;

namespace AtataSamples.SpecFlow.StepDefinitions;

[Binding]
public sealed class CalculationsSteps : BaseSteps
{
    [Given(@"I am on the Calculations page")]
    public static void GivenIAmOnTheCalculationsPage() =>
        Go.To<CalculationsPage>();

    [When(@"I type (.*) and (.*) to the form")]
    public static void WhenITypeArgumentsToTheForm(int argument1, int argument2) =>
        On<CalculationsPage>()
            .AdditionValue1.Set(argument1)
            .AdditionValue2.Set(argument2);

    [Then(@"I should see (.*) in result field")]
    public static void ThenIShouldSeeInResultField(int result) =>
        On<CalculationsPage>()
            .AdditionResult.Should.Equal(result);
}
