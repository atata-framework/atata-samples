using Atata;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace AtataSamples.MultipleBrowsersViaFixtureArguments
{
    public class SignInTests<TDriver> : AutoTestFixture<TDriver>
        where TDriver : RemoteWebDriver
    {
        [Test]
        public void Home()
        {
            Go.To<HomePage>().
                Header.Should.Equal("Atata Sample App");
        }
    }
}
