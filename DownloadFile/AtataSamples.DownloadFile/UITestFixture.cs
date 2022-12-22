﻿using Atata;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AtataSamples.DownloadFile;

[TestFixture]
public class UITestFixture
{
    [SetUp]
    public void SetUp() =>
        AtataContext.Configure().Build();

    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.CleanUp();
}
