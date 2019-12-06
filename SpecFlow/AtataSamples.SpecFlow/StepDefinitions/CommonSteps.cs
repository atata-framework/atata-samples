﻿using Atata;
using AtataSamples.SpecFlow.Components;
using TechTalk.SpecFlow;

namespace AtataSamples.SpecFlow.StepDefinitions
{
    [Binding]
    public sealed class CommonSteps : BaseSteps
    {
        [Given(@"I am on Home Page")]
        public void GivenIAmOnHomePage()
        {
            Go.To<HomePage>();
        }

        [When(@"I navigate to Calculations page by header's button")]
        public void WhenINavigateToCalculationsPageByHeaderSButton()
        {
            On<HomePage>().Calculations.ClickAndGo();
        }

        [When(@"I navigate to Plans page by header's button")]
        public void WhenINavigateToPlansPageByHeaderSButton()
        {
            On<HomePage>().Plans.ClickAndGo();
        }
    }
}
