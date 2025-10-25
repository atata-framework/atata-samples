namespace AtataSamples.ValidationMessagesVerification;

public sealed class ValidationMessageList<TOwner> : AssociatedControlList<ValidationMessage<TOwner>, TOwner>
    where TOwner : PageObject<TOwner>
{
    protected override ValidationMessage<TOwner> CreateAssociatedControl(Control<TOwner> control)
    {
        var validationMessageDefinition = UIComponentResolver.GetControlDefinition(typeof(ValidationMessage<TOwner>));

        PlainScopeLocator scopeLocator = new(control, By.XPath("ancestor::" + validationMessageDefinition.ScopeXPath));

        return Component.Controls.Create<ValidationMessage<TOwner>>(control.ComponentName, scopeLocator);
    }
}
