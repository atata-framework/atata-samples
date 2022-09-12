using Atata;
using NUnit.Framework;

namespace AtataSamples.JQueryUI
{
    public class JQAutocompleteTests : UITestFixture
    {
        [Test]
        public void JQueryUI_Autocomplete() =>
            Go.To<JQAutocompleteDemoPage>()
                .Tags.Set("Python")
                .Tags.Should.Equal("Python")

                .Tags.Clear()
                .Tags.Should.BeNullOrEmpty()

                .Tags.Select("Py")
                .Tags.Should.Equal("Python");
    }
}
