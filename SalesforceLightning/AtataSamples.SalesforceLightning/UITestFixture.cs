﻿using Atata;
using NUnit.Framework;

namespace AtataSamples.SalesforceLightning;

[TestFixture]
public class UITestFixture
{
    [SetUp]
    public void SetUp() =>
        AtataContext.Configure().Build();

    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.Dispose();
}
