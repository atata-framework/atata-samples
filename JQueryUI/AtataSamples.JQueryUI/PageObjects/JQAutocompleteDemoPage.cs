using Atata;

namespace AtataSamples.JQueryUI;

using _ = JQAutocompleteDemoPage;

[Url("/resources/demos/autocomplete/default.html")]
public class JQAutocompleteDemoPage : PageObject<_>
{
    [FindById]
    public JQAutocomplete<_> Tags { get; private set; }
}
