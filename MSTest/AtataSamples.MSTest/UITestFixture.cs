using Atata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AtataSamples.MSTest
{
    public class UITestFixture
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            AtataContext.Configure()
                .UseTestName(TestContext.TestName)
                .LogConsumers.Add(new TextOutputLogConsumer(TestContext.WriteLine))
                .Build();
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
                OnException(exception);
                throw;
            }
        }

        private static void OnException(Exception exception)
        {
            ILogManager log = AtataContext.Current.Log;

            log.Error(exception);

            try
            {
                log.Screenshot("Failed");
            }
            catch (Exception screenshotException)
            {
                log.Error("Take screenshot failed.", screenshotException);
            }
        }
    }
}
