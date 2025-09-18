namespace AtataSamples.NUnit.GenericPageTests;

[ControlDefinition("nav", ComponentTypeName = "menu")]
public class AppMenu<TOwner> : Control<TOwner>
    where TOwner : PageObject<TOwner>
{
    public Link<SignInPage, TOwner> SignIn { get; private set; }
}
