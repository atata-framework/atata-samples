﻿using Atata;
using NUnit.Framework;

namespace AtataSamples.ParallelTestsReusingDrivers;

[SetUpFixture]
public class SetUpFixture
{
    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        AtataContext.GlobalConfiguration
            .UseChrome()
                .WithArguments("start-maximized", "disable-search-engine-choice-screen")
            .UseBaseUrl("https://demo.atata.io/")
            .UseCulture("en-US")
            .UseAllNUnitFeatures();

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }

    [OneTimeTearDown]
    public void GlobalTearDown() =>
        DriverPool.DisposeAll();
}
