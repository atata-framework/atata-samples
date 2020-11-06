using System;
using Atata;
using NUnit.Framework;

namespace AtataSamples.ExtJS
{
    [TestFixture]
    public class UITestFixture
    {
        [SetUp]
        public void SetUp()
        {
            AtataContext.Configure()
                .UseChrome()
                    .WithLocalDriverPath()
                .UseCulture("en-US")
                .UseAllNUnitFeatures()
                .UseElementFindTimeout(TimeSpan.FromSeconds(15))
                .Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }
    }
}
