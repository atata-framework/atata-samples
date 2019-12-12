using Atata;
using NUnit.Framework;

namespace AtataSamples.ParallelTestsReusingDrivers
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class UITestFixture
    {
        /// <summary>Sets up test a test.</summary>
        /// <seealso cref="SetUpFixture.GlobalSetUp"/>
        [SetUp]
        public void SetUp()
        {
            AtataContext.Configure().
                UseDriverPool().
                Build();
        }

        /// <summary>
        /// Tears down a test.
        /// </summary>
        /// <seealso cref="SetUpFixture.GlobalTearDown"/>
        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp(quitDriver: false);
        }
    }
}
