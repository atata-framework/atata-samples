﻿using Atata;
using AtataSamples.Specflow.Components;
using TechTalk.SpecFlow;

namespace AtataSamples.Specflow.StepDefinitions
{
    [Binding]
    public sealed class CalculationsSteps : BaseSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;

        public CalculationsSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"I am on the Caluclations page")]
        public void GivenIAmOnTheCaluclationsPage()
        {
            Go.To<CalculationsPage>();
        }

        [When(@"I type (.*) and (.*) to the form")]
        public void WhenITypeAndToTheForm(int argument1, int argument2)
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
