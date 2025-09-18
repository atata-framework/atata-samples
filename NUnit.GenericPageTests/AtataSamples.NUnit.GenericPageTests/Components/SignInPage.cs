namespace AtataSamples.NUnit.GenericPageTests;

using _ = SignInPage;

public class SignInPage : AppPage<_>, IPageWithHeader<_>
{
    public H1<_> Header { get; private set; }
}
