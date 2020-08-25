using Atata;

namespace AtataSamples.JQueryUI
{
    [ControlDefinition("input", ContainingClass = "ui-autocomplete-input", ComponentTypeName = "autocomplete")]
    public class JQAutocomplete<TOwner> : Input<string, TOwner>
        where TOwner : PageObject<TOwner>
    {
        [FindByClass("ui-autocomplete", ScopeSource = ScopeSource.Page)]
        public UnorderedList<ListItem<TOwner>, TOwner> DropDownItems { get; private set; }

        public TOwner Select(string value)
        {
            Set(value);

            return DropDownItems[x => x.Content.Value.Contains(value)].Click();
        }
    }
}
