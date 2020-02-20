using Atata;

namespace AtataSamples.NUnit.GenericPageTests
{
    public interface IPageWithHeader<TPage>
        where TPage : PageObject<TPage>, IPageWithHeader<TPage>
    {
        H1<TPage> Header { get; }
    }
}
