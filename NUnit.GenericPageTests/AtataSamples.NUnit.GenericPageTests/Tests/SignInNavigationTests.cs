using Atata;
using NUnit.Framework;

namespace AtataSamples.NUnit.GenericPageTests;

/// <summary>
/// An example of fixture that executes the same workflow scenario against different similar pages.
/// The <typeparamref name="TPage"/> page type should inherit <see cref="AppPage{TOwner}"/>.
/// </summary>
/// <typeparam name="TPage">The type of the page.</typeparam>
[TestFixture(typeof(PlansPage))]
[TestFixture(typeof(ProductsPage))]
public class SignInNavigationTests<TPage> : UITestFixture
    where TPage : AppPage<TPage>
{
    [Test]
    public void Test() =>
        Go.To<TPage>()
            .Menu.SignIn.ClickAndGo()
                .Header.Should.StartWith("Sign In");
}
