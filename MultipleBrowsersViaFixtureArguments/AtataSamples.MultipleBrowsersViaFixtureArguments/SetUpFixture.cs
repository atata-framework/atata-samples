﻿using Atata;
using Atata.WebDriverSetup;
using NUnit.Framework;

namespace AtataSamples.MultipleBrowsersViaFixtureArguments
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            AtataContext.GlobalConfiguration
                .UseChrome()
                    .WithArguments("start-maximized")
                .UseEdge()

                // TODO: You can also specify remote driver configuration(s):
                // .UseRemoteDriver()
                // .WithAlias("chrome_remote")
                // .WithRemoteAddress("http://127.0.0.1:4444/wd/hub")
                // .WithOptions(new ChromeOptions())
                .UseBaseUrl("https://demo.atata.io/")
                .UseCulture("en-US")
                .UseAllNUnitFeatures();

            DriverSetup.GetDefaultConfiguration(BrowserNames.InternetExplorer)
                .WithX32Architecture();

            AtataContext.GlobalConfiguration.AutoSetUpConfiguredDrivers();
        }
    }
}
