using Atata;
using NUnit.Framework;

namespace AtataSamples.JsonConfiguration.MultiBrowserViaFixtureArguments
{
    public class HomeTests : UITestFixture
    {
        public HomeTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [Test]
        public void Home() =>
            Go.To<HomePage>()
                .Header.Should.Equal("Atata Sample App");
    }
}
