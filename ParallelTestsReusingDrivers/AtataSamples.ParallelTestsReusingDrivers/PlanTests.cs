using Atata;
using NUnit.Framework;

namespace AtataSamples.ParallelTestsReusingDrivers
{
    public class PlanTests : UITestFixture
    {
        [TestCase("Basic")]
        [TestCase("Plus")]
        [TestCase("Premium")]
        public void Plans_Has(string title)
        {
            Go.To<PlansPage>().
                PlanItems[x => x.Title == title].Should.BeVisible();
        }
    }
}
