namespace AtataSamples.SalesforceLightning;

[ControlDefinition(ComponentTypeName = "combobox")]
public class SLCombobox<T, TOwner> : EditableField<T, TOwner>
    where TOwner : PageObject<TOwner>
{
    [FindByClass("slds-dropdown", OuterXPath = "parent::*/following-sibling::")]
    public ItemsControl<DropDownOption, TOwner> DropDownList { get; private set; }

    protected override T GetValue()
    {
        string valueAsString = TagName == "input"
            ? DomProperties.Value
            : Content;

        return ConvertStringToValueUsingGetFormat(valueAsString);
    }

    protected override void SetValue(T value)
    {
        Click();

        string valueAsString = ConvertValueToStringUsingSetFormat(value);
        DropDownList.Items[x => x == valueAsString].Click();
    }

    [ControlDefinition(ContainingClass = "slds-media__body", ComponentTypeName = "option")]
    public class DropDownOption : Text<TOwner>
    {
    }
}
