using System;
using System.Collections;
using System.Linq;
using Atata;

namespace AtataSamples.ExtJS;

[ControlDefinition("input[@role='combobox']", ContainingClass = "x-form-text", ComponentTypeName = "combo box")]
public class ExtComboBox<TOwner> : Input<string, TOwner>
    where TOwner : PageObject<TOwner>
{
    private const string ScriptToGetOptionValues = @"
var component = Ext.getCmp(arguments[0].getAttribute('data-componentid'));
var displayField = component.displayField;
var dataItems = component.store.data.items;

var results = [];
for (var i = 0; i < dataItems.length; i++) {
  results.push(dataItems[i].data[displayField]);
}
return results;";

    public UnorderedList<ListItem<TOwner>, TOwner> DropDownList =>
        Controls.Resolve<UnorderedList<ListItem<TOwner>, TOwner>>(nameof(DropDownList), () =>
        {
            string componentId = DomAttributes["data-componentid"];

            return new Attribute[]
            {
                new FindByIdAttribute($"{componentId}-picker") { ScopeSource = ScopeSource.Page }
            };
        });

    [FindFirst(OuterXPath = "parent::*/following-sibling::")]
    [ControlDefinition("div", ContainingClass = "x-form-trigger")]
    public Control<TOwner> PickerTrigger { get; private set; }

    public ValueProvider<string[], TOwner> Options =>
        CreateValueProvider(
            "options",
            () => Script.ExecuteAgainst<IEnumerable>(ScriptToGetOptionValues).Value.Cast<string>().ToArray());

    public TOwner Select(string value)
    {
        Clear();

        PickerTrigger.Click();

        return DropDownList.Items.GetByXPathCondition(value, $".='{value}'").Click();
    }
}
