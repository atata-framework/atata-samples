namespace AtataSamples.DevExtreme;

[ControlDefinition(ContainingClass = "dx-selectbox", ComponentTypeName = "select box")]
public class DXSelectBox<T, TOwner> : EditableField<T, TOwner>
    where TOwner : PageObject<TOwner>
{
    [FindFirst]
    [TraceLog]
    [Name("Associated")]
    protected TextInput<TOwner> AssociatedInput { get; private set; }

    [FindByClass("dx-overlay-content", ScopeSource = ScopeSource.Page)]
    public ItemsControl<Option, TOwner> DropDownList { get; private set; }

    protected override T GetValue()
    {
        string? valueAsString = AssociatedInput.Value;

        if (valueAsString.Length == 0)
            valueAsString = null;

        return ConvertStringToValueUsingGetFormat(valueAsString);
    }

    protected override void SetValue(T value)
    {
        string valueAsString = ConvertValueToStringUsingSetFormat(value);

        Click();
        DropDownList.Items.GetByXPathCondition(valueAsString, $". = '{valueAsString}'").Click();
    }

    protected override bool GetIsReadOnly() =>
        DomClasses.Contains(DXDomClasses.ReadOnly);

    protected override bool GetIsEnabled() =>
        !DomClasses.Contains(DXDomClasses.Disabled);

    [ControlDefinition(ContainingClass = "dx-list-item", ComponentTypeName = "option")]
    [GetsContentFromSource(ContentSource.TextContent)]
    [ScrollTo]
    public sealed class Option : Text<TOwner>
    {
    }
}
