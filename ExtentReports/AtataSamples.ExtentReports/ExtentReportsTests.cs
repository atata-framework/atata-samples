using Atata;
using NUnit.Framework;

namespace AtataSamples.ExtentReports
{
    public class ExtentReportsTests : UITestFixture
    {
        [Test]
        public void ExtentReports_Test1()
        {
            Go.To<HomePage>()
                .Report.Screenshot()
                .Header.Should.Contain("Atata");
        }

        [Test]
        public void ExtentReports_Test2()
        {
            Go.To<HomePage>()
                .Report.Screenshot()
                .AggregateAssert(x => x
                    .PageTitle.Should.Contain("Atata")
                    .Header.Should.Contain("Atata"));
        }
    }
}
