using Atata;
using NUnit.Framework;

namespace AtataSamples.ConfirmationPopups
{
    public class ProductTests : UITestFixture
    {
        [Test]
        public void Products_DeleteUsingJSConfirm()
        {
            int count;

            Go.To<ProductsPage>().
                Products.Rows.Count.Get(out count).

                Products.Rows[x => x.Name == "Table"].DeleteUsingJSConfirm().
                Products.Rows[x => x.Name == "Table"].Should.Not.Exist().
                Products.Rows.Count.Should.Equal(count - 1);
        }

        [Test]
        public void Products_DeleteUsingBSModal()
        {
            int count;

            Go.To<ProductsPage>().
                Products.Rows.Count.Get(out count).

                Products.Rows[x => x.Name == "Chair"].DeleteUsingBSModal().
                    Cancel(). // Cancel and verify that nothing is deleted.
                Products.Rows[x => x.Name == "Chair"].Should.Exist().
                Products.Rows.Count.Should.Equal(count).

                Products.Rows[x => x.Name == "Chair"].DeleteUsingBSModal().
                    Delete(). // Delete and verify that item is deleted.
                Products.Rows[x => x.Name == "Chair"].Should.Not.Exist().
                Products.Rows.Count.Should.Equal(count - 1);
        }

        [Test]
        public void Products_DeleteUsingBSModal_ViaTrigger()
        {
            int count;

            Go.To<ProductsPage>().
                Products.Rows.Count.Get(out count).

                Products.Rows[x => x.Name == "Chair"].DeleteUsingBSModalViaTrigger().
                Products.Rows[x => x.Name == "Chair"].Should.Not.Exist().
                Products.Rows.Count.Should.Equal(count - 1);
        }

        [Test]
        public void Products_DeleteUsingJQueryConfirm()
        {
            int count;

            Go.To<ProductsPage>().
                Products.Rows.Count.Get(out count).

                Products.Rows[x => x.Name == "Desk"].DeleteUsingJQueryConfirm().
                    Cancel(). // Cancel and verify that nothing is deleted.
                Products.Rows[x => x.Name == "Desk"].Should.Exist().
                Products.Rows.Count.Should.Equal(count).

                Products.Rows[x => x.Name == "Desk"].DeleteUsingJQueryConfirm().
                    Delete(). // Delete and verify that item is deleted.
                Products.Rows[x => x.Name == "Desk"].Should.Not.Exist().
                Products.Rows.Count.Should.Equal(count - 1);
        }

        [Test]
        public void Products_DeleteUsingJQueryConfirm_ViaTrigger()
        {
            int count;

            Go.To<ProductsPage>().
                Products.Rows.Count.Get(out count).

                Products.Rows[x => x.Name == "Desk"].DeleteUsingJQueryConfirmViaTrigger().
                Products.Rows[x => x.Name == "Desk"].Should.Not.Exist().
                Products.Rows.Count.Should.Equal(count - 1);
        }
    }
}
