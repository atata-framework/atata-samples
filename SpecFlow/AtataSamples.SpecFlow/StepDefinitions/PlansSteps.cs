using Atata;
using AtataSamples.SpecFlow.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace AtataSamples.SpecFlow.StepDefinitions
{
    [Binding]
    public sealed class PlansSteps : BaseSteps
    {

        private readonly ScenarioContext context;

        public PlansSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Then(@"I verify Plans page")]
        public void ThenIVerifyPlansPage()
        {
            On<PlansPage>().PlanItems.Count.Should.Equal(3);
        }
    }
}
