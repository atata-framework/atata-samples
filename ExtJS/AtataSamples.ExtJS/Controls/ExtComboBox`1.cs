using System;
using Atata;

namespace AtataSamples.ExtJS
{
    [ControlDefinition("input[@role='combobox']", ContainingClass = "x-form-text", ComponentTypeName = "combo box")]
    public class ExtComboBox<TOwner> : Input<string, TOwner>
        where TOwner : PageObject<TOwner>
    {
        public UnorderedList<ListItem<TOwner>, TOwner> DropDownItems =>
            Controls.Resolve<UnorderedList<ListItem<TOwner>, TOwner>>(nameof(DropDownItems), () =>
            {
                string componentId = Attributes["data-componentid"];

                return new Attribute[]
                {
                    new FindByIdAttribute($"{componentId}-picker") { ScopeSource = ScopeSource.Page}
                };
            });

        [FindFirst(OuterXPath = "parent::*/following-sibling::")]
        [ControlDefinition("div", ContainingClass = "x-form-trigger")]
        public Control<TOwner> PickerTrigger { get; private set; }

        public TOwner Select(string value)
        {
            Clear();

            PickerTrigger.Click();

            return DropDownItems.Items.GetByXPathCondition(value, $".='{value}'").Click();
        }
    }
}
