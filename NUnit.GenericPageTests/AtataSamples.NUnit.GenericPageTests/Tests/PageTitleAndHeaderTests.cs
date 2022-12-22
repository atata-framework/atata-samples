using Atata;
using NUnit.Framework;

namespace AtataSamples.NUnit.GenericPageTests;

/// <summary>
/// An example of fixture that verifies the page title and header.
/// The <typeparamref name="TPage"/> page type should inherit <see cref="PageObject{TOwner}"/> and implement <see cref="IPageWithHeader{TPage}"/>,
/// that allows us to use common pages' control as <see cref="IPageWithHeader{TPage}.Header"/> in this example.
/// </summary>
/// <typeparam name="TPage">The type of the page.</typeparam>
[TestFixture(typeof(PlansPage), "Plans")]
[TestFixture(typeof(ProductsPage), "Products")]
public class PageTitleAndHeaderTests<TPage> : UITestFixture
    where TPage : PageObject<TPage>, IPageWithHeader<TPage>
{
    private readonly string _expectedPageTitle;

    public PageTitleAndHeaderTests(string expectedPageTitle) =>
        _expectedPageTitle = expectedPageTitle;

    [Test]
    public void Test() =>
        Go.To<TPage>()
            .AggregateAssert(x => x
                .PageTitle.Should.Equal($"{_expectedPageTitle} - Atata Sample App")
                .Header.Should.Equal(_expectedPageTitle));
}
