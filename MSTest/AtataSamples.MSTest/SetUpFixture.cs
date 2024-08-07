using Atata;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtataSamples.MSTest;

[TestClass]
public static class SetUpFixture
{
    [AssemblyInitialize]
    public static void GlobalSetUp(TestContext testContext)
    {
        AtataContext.GlobalConfiguration
            .UseChrome()
                .WithArguments("start-maximized", "disable-search-engine-choice-screen")
            .UseBaseUrl("https://demo.atata.io/")
            .UseCulture("en-US");

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }
}
