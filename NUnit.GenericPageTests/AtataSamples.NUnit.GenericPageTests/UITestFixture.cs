using Atata;
using NUnit.Framework;

namespace AtataSamples.NUnit.GenericPageTests
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
                Build();
        }

        /// <summary>
        /// Tears down a test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }
    }
}
