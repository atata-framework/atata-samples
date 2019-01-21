# Performance of ControlList

[![Docs](https://img.shields.io/badge/Download-Sources-brightgreen.svg)](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/atata-framework/atata-samples/tree/master/Performance.ControlList)

Demonstrates the performance practices to enumerate a big list of controls (500 `<tr>` elements).

## Examples

### Page Object

```cs
using System.Collections.Generic;
using Atata;

namespace AtataSamples.Performance.ControlList
{
    using _ = TableListPage;

    [Url("table-list")]
    public class TableListPage : Page<_>
    {
        public ItemsContainer Items { get; set; }

        [ControlDefinition("div", ContainingClass = "table-list", ComponentTypeName = "list")]
        public class ItemsContainer : Control<_>
        {
            public ExtendedControlList<ItemRow, _> Rows { get; private set; }

            public DataProvider<IEnumerable<int>, _> Ids
                => Rows.SelectContents<int>("Ids", ItemRow.XPathTo.Id);

            public DataProvider<IEnumerable<string>, _> Names
                => Rows.SelectContents("Names", ItemRow.XPathTo.Name);

            public ItemRow FindRowById(int id)
            {
                return Rows.GetByXPathCondition($"Id={id}", $"{ItemRow.XPathTo.Id}[.='{id}']");
            }

            public ItemRow FindRowByIdAndName(int id, string name)
            {
                return Rows.GetByXPathCondition($"Id={id} & Name={name}", $"{ItemRow.XPathTo.Id}[.='{id}'] and {ItemRow.XPathTo.Name}[.='{name}']");
            }
        }

        public class ItemRow : TableRow<_>
        {
            [FindByXPath(XPathTo.Id)]
            public Number<_> Id { get; private set; }

            [FindByXPath(XPathTo.Name)]
            public Text<_> Name { get; private set; }

            [FindByClass]
            public Text<_> Description { get; private set; }

            public static class XPathTo
            {
                public const string Id = "./td[contains(concat(' ', normalize-space(@class), ' '), ' id ')]";

                public const string Name = "./td[contains(concat(' ', normalize-space(@class), ' '), ' name ')]";
            }
        }
    }
}
```

### Tests

```cs
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

        [Test]
        public void TableList_VerifyItemNameById_Fast()
        {
            Go.To<TableListPage>().
                Items.FindRowById(250).Name.Should.Equal("Item 250");
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
```