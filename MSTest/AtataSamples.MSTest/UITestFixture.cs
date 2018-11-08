using System;
using Atata;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtataSamples.MSTest
{
    public class UITestFixture
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            AtataContext.Configure().
                UseChrome().
                UseBaseUrl("https://atata-framework.github.io/atata-sample-app/#!/").
                UseTestName(TestContext.TestName).
                AddTraceLogging().
                Build();
        }

        [TestCleanup]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }

        protected void Execute(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception exception)
            {
                AtataContext.Current.Log.Error(null, exception);
                throw;
            }
        }
    }
}
