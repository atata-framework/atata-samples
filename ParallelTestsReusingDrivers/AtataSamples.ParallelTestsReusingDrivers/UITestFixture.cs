using Atata;
using NUnit.Framework;

namespace AtataSamples.ParallelTestsReusingDrivers
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class UITestFixture
    {
        /// <summary>Sets up test.</summary>
        /// <seealso cref="SetUpFixture.GlobalSetUp"/>
        [SetUp]
        public void SetUp()
        {
            AtataContext.Configure().
                UseDriverPool().
                Build();
        }

        [TearDown]
        public void TearDown()
        {
            DriverPool.Release(AtataContext.Current?.Driver);
            AtataContext.Current?.CleanUp(quitDriver: false);
        }
    }
}
