using System;
using Atata;
using NUnit.Framework;

namespace AtataSamples.MultipleBrowsersViaFixtureArguments
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            AtataContext.GlobalConfiguration.
                UseChrome().
                    WithArguments("start-maximized").
                    WithDriverPath(Environment.GetEnvironmentVariable("ChromeDriver") ?? AppDomain.CurrentDomain.BaseDirectory).
                UseInternetExplorer().
                    // TODO: Specify Internet Explorer settings, like:
                    // WithOptions(x => x.EnableNativeEvents = true).
                    WithDriverPath(Environment.GetEnvironmentVariable("IEWebDriver") ?? AppDomain.CurrentDomain.BaseDirectory).
                UseFirefox().
                    WithFixOfCommandExecutionDelay().
                    WithDriverPath(Environment.GetEnvironmentVariable("GeckoWebDriver") ?? AppDomain.CurrentDomain.BaseDirectory).
                // TODO: You can also specify remote driver configuration(s):
                // UseRemoteDriver().
                // WithAlias("chrome_remote").
                // WithRemoteAddress("http://127.0.0.1:4444/wd/hub").
                // WithOptions(new ChromeOptions()).
                UseBaseUrl("https://demo.atata.io/").
                UseAllNUnitFeatures();
        }
    }
}
