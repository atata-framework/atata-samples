namespace AtataSamples.JQueryUI;

public sealed class JQAutocompleteTests : UITestFixture
{
    [Test]
    public void JQueryUI_Autocomplete() =>
        Go.To<JQAutocompleteDemoPage>()
            .Tags.Set("Python")
            .Tags.Should.Be("Python")

            .Tags.Clear()
            .Tags.Should.BeNullOrEmpty()

            .Tags.Select("Py")
            .Tags.Should.Be("Python");
}
