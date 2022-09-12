using Atata;
using NUnit.Framework;

namespace AtataSamples.NetCore3.NUnit
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class UITestFixture
    {
        [SetUp]
        public void SetUp() =>
            AtataContext.Configure().Build();

        [TearDown]
        public void TearDown() =>
            AtataContext.Current?.CleanUp();
    }
}
