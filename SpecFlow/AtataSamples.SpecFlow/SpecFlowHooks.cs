using Atata;
using TechTalk.SpecFlow;

namespace AtataSamples.SpecFlow
{
    [Binding]
    public sealed class SpecFlowHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeTestRun]
        public static void SetUpTestRun()
        {
            AtataContext.GlobalConfiguration.
                UseChrome().
                    WithArguments("start-maximized").
                    WithLocalDriverPath().
                UseBaseUrl("https://demo.atata.io/").
                UseCulture("en-us").
                UseAllNUnitFeatures();
        }

        [BeforeScenario]
        public static void SetUpScenario()
        {
            AtataContext.Configure().
                Build();
        }

        [AfterScenario]
        public static void TearDownScenario()
        {
            AtataContext.Current?.CleanUp();
        }
    }
}
