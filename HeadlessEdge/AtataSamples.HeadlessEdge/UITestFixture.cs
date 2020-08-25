using System;
using Atata;
using NUnit.Framework;
using OpenQA.Selenium.Edge;

namespace AtataSamples.HeadlessEdge
{
    [TestFixture]
    public class UITestFixture
    {
        [SetUp]
        public void SetUp()
        {
            AtataContext.Configure().
                UseDriver(() =>
                {
                    EdgeOptions options = new EdgeOptions
                    {
                        UseChromium = true
                    };

                    options.AddArguments("headless", "disable-gpu", "window-size=1024,768");

                    return new EdgeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
                }).
                UseBaseUrl("https://demo.atata.io/").
                UseCulture("en-US").
                UseAllNUnitFeatures().
                Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }
    }
}
