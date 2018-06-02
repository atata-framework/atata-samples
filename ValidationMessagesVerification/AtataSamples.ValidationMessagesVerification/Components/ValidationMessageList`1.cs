using System;
using Atata;
using OpenQA.Selenium;

namespace AtataSamples.ValidationMessagesVerification
{
    public class ValidationMessageList<TOwner> : ControlList<ValidationMessage<TOwner>, TOwner>
        where TOwner : PageObject<TOwner>
    {
        public ValidationMessage<TOwner> this[Func<TOwner, IControl<TOwner>> controlSelector]
        {
            get { return For(controlSelector); }
        }

        public ValidationMessage<TOwner> For(Func<TOwner, IControl<TOwner>> controlSelector)
        {
            var validationMessageDefinition = UIComponentResolver.GetControlDefinition(typeof(ValidationMessage<TOwner>));

            IControl<TOwner> boundControl = controlSelector(Component.Owner);

            PlainScopeLocator scopeLocator = new PlainScopeLocator(By.XPath("ancestor::" + validationMessageDefinition.ScopeXPath))
            {
                SearchContext = boundControl.Scope
            };

            return Component.Controls.Create<ValidationMessage<TOwner>>(boundControl.ComponentName, scopeLocator);
        }
    }
}
