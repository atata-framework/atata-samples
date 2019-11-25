using Atata;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace AtataSamples.Specflow
{
    [Binding]
    public sealed class SpecFlowHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeTestRun]
        public static void TestRunSetup()
        {
            AtataContext.Configure().
                UseChrome().
                WithArguments("start-maximized").
                
                UseBaseUrl("https://demo.atata.io/").
                UseCulture("en-us").
                UseNUnitTestName().
                AddNUnitTestContextLogging().
                    WithoutSectionFinish().
                LogNUnitError().
                Build();
        }

        [AfterTestRun]
        public static void TestRunTeardown()
        {
            AtataContext.Current?.CleanUp();
        }      
    }
}
