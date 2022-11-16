﻿using System;
using Atata;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtataSamples.MSTest
{
    public class UITestFixture
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void SetUp() =>
            AtataContext.Configure()
                .UseTestName(TestContext.TestName)
                .LogConsumers.Add(new TextOutputLogConsumer(TestContext.WriteLine))
                .Build();

        [TestCleanup]
        public void TearDown() =>
            AtataContext.Current?.CleanUp();

        protected static void Execute(Action action)
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
            var context = AtataContext.Current;

            context.Log.Error(exception);

            context.TakeScreenshot("Failed");
            context.TakePageSnapshot("Failed");
        }
    }
}
