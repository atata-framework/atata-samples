using Atata;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtataSamples.MSTest
{
    [TestClass]
    public class SampleTests : UITestFixture
    {
        [TestMethod]
        public void MSTest()
        {
            Go.To<HomePage>().
               Header.Should.Equal("Atata Sample App");
        }
    }
}
