using Atata;
using NUnit.Framework;

namespace AtataSamples.Performance.ControlList
{
    public class TableListTests : UITestFixture
    {
        [Test]
        public void TableList_VerifyNoItemWithId_Fast()
        {
            Go.To<TableListPage>().
                Items.Ids.Should.Not.Contain(999);
        }

        [Test, Explicit("Runs slowly about 30 seconds.")]
        public void TableList_VerifyNoItemWithId_Classic()
        {
            Go.To<TableListPage>().
                Items.Rows.Should.Not.Contain(x => x.Id == 999);
        }

        [Test, Explicit("Runs slowly about 30 seconds.")]
        public void TableList_VerifyNoItemWithId_Alternative_Exist()
        {
            Go.To<TableListPage>().
                Items.Rows[x => x.Id == 999].Should.Not.Exist();
        }

        [Test, Explicit("Runs slowly about 30 seconds.")]
        public void TableList_VerifyNoItemWithId_Alternative_SelectData()
        {
            Go.To<TableListPage>().
                Items.Rows.SelectData(x => x.Id).Should.Not.Contain(999);
        }

        [Test]
        public void TableList_VerifyNoItemWithName_Fast()
        {
            Go.To<TableListPage>().
                Items.Names.Should.Not.Contain("Unknown name");
        }

        [Test]
        public void TableList_VerifyItemWithName_Fast()
        {
            Go.To<TableListPage>().
                Items.Names.Should.Contain("Item 250");
        }

        [Test, Explicit("Runs slowly about 20 seconds.")]
        public void TableList_VerifyItemWithName_Classic()
        {
            Go.To<TableListPage>().
                Items.Rows.Should.Contain(x => x.Name == "Item 250");
        }

        [Test]
        public void TableList_VerifyItemNameById_Fast()
        {
            Go.To<TableListPage>().
                Items.FindRowById(250).Name.Should.Equal("Item 250");
        }

        [Test, Explicit("Runs slowly about 20 seconds.")]
        public void TableList_VerifyItemNameById_Classic()
        {
            Go.To<TableListPage>().
                Items.Rows[x => x.Id == 250].Name.Should.Equal("Item 250");
        }

        [Test]
        public void TableList_VerifyItemByIdAndName_Fast()
        {
            Go.To<TableListPage>().
                Items.FindRowByIdAndName(450, "Item 450").Should.BeVisible();
        }

        [Test]
        public void TableList_VerifyNoItemByIdAndName_Fast()
        {
            Go.To<TableListPage>().
                Items.FindRowByIdAndName(999, "Item 999").Should.Not.BeVisible();
        }
    }
}
