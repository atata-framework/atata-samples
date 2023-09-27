using Atata;
using NUnit.Framework;

namespace AtataSamples.ConfirmationPopups;

public class ProductTests : UITestFixture
{
    [Test]
    public void DeleteUsingJSConfirm() =>
        Go.To<ProductsPage>()
            .Products.Rows.Count.Get(out int count)

            .Products.Rows[x => x.Name == "Table"].DeleteUsingJSConfirm()
            .Products.Rows[x => x.Name == "Table"].Should.Not.BePresent()
            .Products.Rows.Count.Should.Equal(count - 1);

    [Test]
    public void DeleteUsingBSModal() =>
        Go.To<ProductsPage>()
            .Products.Rows.Count.Get(out int count)

            .Products.Rows[x => x.Name == "Chair"].DeleteUsingBSModal()
                .Cancel() // Cancel and verify that nothing is deleted.
            .Products.Rows[x => x.Name == "Chair"].Should.BePresent()
            .Products.Rows.Count.Should.Equal(count)

            .Products.Rows[x => x.Name == "Chair"].DeleteUsingBSModal()
                .Delete() // Delete and verify that item is deleted.
            .Products.Rows[x => x.Name == "Chair"].Should.Not.BePresent()
            .Products.Rows.Count.Should.Equal(count - 1);

    [Test]
    public void DeleteUsingBSModal_ViaTrigger() =>
        Go.To<ProductsPage>()
            .Products.Rows.Count.Get(out int count)

            .Products.Rows[x => x.Name == "Chair"].DeleteUsingBSModalViaTrigger()
            .Products.Rows[x => x.Name == "Chair"].Should.Not.BePresent()
            .Products.Rows.Count.Should.Equal(count - 1);

    [Test]
    public void DeleteUsingJQueryConfirm() =>
        Go.To<ProductsPage>()
            .Products.Rows.Count.Get(out int count)

            .Products.Rows[x => x.Name == "Desk"].DeleteUsingJQueryConfirm()
                .Cancel() // Cancel and verify that nothing is deleted.
            .Products.Rows[x => x.Name == "Desk"].Should.BePresent()
            .Products.Rows.Count.Should.Equal(count)

            .Products.Rows[x => x.Name == "Desk"].DeleteUsingJQueryConfirm()
                .Delete() // Delete and verify that item is deleted.
            .Products.Rows[x => x.Name == "Desk"].Should.Not.BePresent()
            .Products.Rows.Count.Should.Equal(count - 1);

    [Test]
    public void DeleteUsingJQueryConfirm_ViaTrigger() =>
        Go.To<ProductsPage>()
            .Products.Rows.Count.Get(out int count)

            .Products.Rows[x => x.Name == "Desk"].DeleteUsingJQueryConfirmViaTrigger()
            .Products.Rows[x => x.Name == "Desk"].Should.Not.BePresent()
            .Products.Rows.Count.Should.Equal(count - 1);
}
