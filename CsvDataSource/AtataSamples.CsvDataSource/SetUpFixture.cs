﻿using Atata;
using NUnit.Framework;

namespace AtataSamples.CsvDataSource;

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
}
