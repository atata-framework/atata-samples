using Atata;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace AtataSamples.FixtureReusingDriver
{
    [TestFixture]
    public class UITestFixture
    {
        protected virtual bool ReuseDriver => false;

        protected RemoteWebDriver PreservedDriver { get; private set; }

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            if (ReuseDriver)
                PreservedDriver = AtataContext.GlobalConfiguration.BuildingContext.DriverFactoryToUse.Create();
        }

        [SetUp]
        public void SetUp()
        {
            AtataContextBuilder contextBuilder = AtataContext.Configure();

            if (ReuseDriver && PreservedDriver != null)
                contextBuilder = contextBuilder.UseDriver(PreservedDriver);

            contextBuilder.Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp(!ReuseDriver);
        }

        [OneTimeTearDown]
        public void TearDownFixture()
        {
            if (PreservedDriver != null)
            {
                PreservedDriver.Dispose();
                PreservedDriver = null;
            }
        }
    }
}
