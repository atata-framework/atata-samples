using Atata;
using NUnit.Framework;

namespace AtataSamples.HeadlessEdge
{
    public class SampleTests : UITestFixture
    {
        [Test]
        public void Edge_Headless()
        {
            Go.To<OrdinaryPage>().PageTitle.Should.Contain("Atata");
        }
    }
}
