namespace AtataSamples.NUnit.GenericPageTests;

/// <summary>
/// An example of fixture that verifies the page title.
/// The <typeparamref name="TPage"/> page type should inherit <see cref="PageObject{TOwner}"/>, meaning any page object type.
/// </summary>
/// <typeparam name="TPage">The type of the page.</typeparam>
[TestFixture(typeof(PlansPage), "Plans")]
[TestFixture(typeof(ProductsPage), "Products")]
public sealed class PageTitleTests<TPage> : UITestFixture
    where TPage : PageObject<TPage>
{
    private readonly string _expectedPageTitle;

    public PageTitleTests(string expectedPageTitle) =>
        _expectedPageTitle = expectedPageTitle;

    [Test]
    public void Test() =>
        Go.To<TPage>()
            .PageTitle.Should.Equal($"{_expectedPageTitle} - Atata Sample App");
}
