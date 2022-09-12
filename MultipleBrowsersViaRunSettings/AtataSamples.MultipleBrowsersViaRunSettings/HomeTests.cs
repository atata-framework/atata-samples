using Atata;
using NUnit.Framework;

namespace AtataSamples.MultipleBrowsersViaRunSettings
{
    public class HomeTests : UITestFixture
    {
        [Test]
        public void Home() =>
            Go.To<HomePage>()
                .Header.Should.Equal("Atata Sample App");
    }
}
