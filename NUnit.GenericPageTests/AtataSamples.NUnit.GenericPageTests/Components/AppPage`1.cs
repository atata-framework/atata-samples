namespace AtataSamples.NUnit.GenericPageTests;

public class AppPage<TOwner> : Page<TOwner>
    where TOwner : AppPage<TOwner>
{
    public AppMenu<TOwner> Menu { get; private set; }
}
