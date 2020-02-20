using Atata;

namespace AtataSamples.NUnit.GenericPageTests
{
    using _ = ProductsPage;

    [Url("products")]
    public class ProductsPage : AppPage<_>, IPageWithHeader<_>
    {
        public H1<_> Header { get; private set; }
    }
}
