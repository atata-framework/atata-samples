using System;
using System.Collections.Generic;
using Atata;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace AtataSamples.MultipleBrowsersViaFixtureArguments
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    public class UITestFixture<TDriver>
        where TDriver : RemoteWebDriver
    {
        [SetUp]
        public void SetUp()
        {
            AtataContext.Configure().
                UseDriver(() =>
                    (RemoteWebDriver)Activator.CreateInstance(typeof(TDriver), CreateDriverParameters())).
                UseBaseUrl("https://atata-framework.github.io/atata-sample-app/#!/").
                UseTestName($"{TestContext.CurrentContext.Test.Name}[{typeof(TDriver).Name}]").
                AddNUnitTestContextLogging().
                    WithoutSectionFinish().
                LogNUnitError().
                Build();
        }

        private object[] CreateDriverParameters()
        {
            Type driverType = typeof(TDriver);
            List<object> parameters = new List<object>();

            if (driverType == typeof(ChromeDriver))
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("disable-extensions", "no-sandbox", "start-maximized");
                parameters.Add(options);
            }
            else if (driverType == typeof(InternetExplorerDriver))
            {
                //InternetExplorerDriverService driverService = InternetExplorerDriverService.CreateDefaultService("driver path...");
                //parameters.Add(driverService);

                InternetExplorerOptions options = new InternetExplorerOptions();
                //// Set some options properties.
                parameters.Add(options);
            }
            return parameters.ToArray();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current.CleanUp();
        }
    }
}
