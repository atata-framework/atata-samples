using Atata;
using AtataSamples.SpecFlow.Components;
using TechTalk.SpecFlow;

namespace AtataSamples.SpecFlow.StepDefinitions
{
    [Binding]
    public sealed class CalculationsSteps : BaseSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        [Given(@"I am on the Calculations page")]
        public void GivenIAmOnTheCalculationsPage()
        {
            Go.To<CalculationsPage>();
        }

        [When(@"I type (.*) and (.*) to the form")]
        public void WhenITypeArgumentsToTheForm(int argument1, int argument2)
        {
            On<CalculationsPage>().
                AdditionValue1.Set(argument1).
                AdditionValue2.Set(argument2);
        }

        [Then(@"I should see (.*) in result field")]
        public void ThenIShouldSeeInResultField(int result)
        {
            On<CalculationsPage>().AdditionResult.Should.Equal(result);
        }
    }
}
