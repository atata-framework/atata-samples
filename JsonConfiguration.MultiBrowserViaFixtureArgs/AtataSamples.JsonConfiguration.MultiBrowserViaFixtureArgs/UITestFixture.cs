﻿using Atata;
using NUnit.Framework;

namespace AtataSamples.JsonConfiguration.MultiBrowserViaFixtureArguments
{
    [TestFixture(DriverAliases.Chrome)]
    [TestFixture(DriverAliases.InternetExplorer)]
    //[TestFixture(DriverAliases.Firefox)]
    //[TestFixture("chrome_remote")]
    [Parallelizable]
    public abstract class UITestFixture
    {
        private readonly string driverAlias;

        protected UITestFixture(string driverAlias)
        {
            this.driverAlias = driverAlias;
        }

        [SetUp]
        public void SetUp()
        {
            AtataContext.Configure().
                UseDriver(driverAlias).
                UseTestName(() => $"[{driverAlias}]{TestContext.CurrentContext.Test.Name}").
                Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }
    }
}
