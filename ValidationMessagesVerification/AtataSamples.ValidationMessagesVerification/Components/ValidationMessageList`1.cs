namespace AtataSamples.ValidationMessagesVerification;

public class ValidationMessageList<TOwner> : AssociatedControlList<ValidationMessage<TOwner>, TOwner>
    where TOwner : PageObject<TOwner>
{
    protected override ValidationMessage<TOwner> CreateAssociatedControl(Control<TOwner> control)
    {
        var validationMessageDefinition = UIComponentResolver.GetControlDefinition(typeof(ValidationMessage<TOwner>));

        PlainScopeLocator scopeLocator = new(By.XPath("ancestor::" + validationMessageDefinition.ScopeXPath))
        {
            SearchContext = control.Scope
        };

        return Component.Controls.Create<ValidationMessage<TOwner>>(control.ComponentName, scopeLocator);
    }
}
