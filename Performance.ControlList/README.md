# [Atata Samples](https://github.com/atata-framework/atata-samples) / Performance Practices for ControlList

[![Download sources](https://img.shields.io/badge/Download-sources-brightgreen.svg)](https://github.com/atata-framework/atata-samples/raw/master/_archives/Performance.ControlList.zip)

Demonstrates the performance practices to enumerate a big list of controls (500 `<tr>` elements).
Query execution time was decreased from 60 seconds to just 0.3 seconds for a single operation in scope of entire list.

*[Download sources](https://github.com/atata-framework/atata-samples/raw/master/_archives/Performance.ControlList.zip), run tests, check results and experiment with [Atata Framework](https://atata.io).*

## Page Under Test

<https://demo.atata.io/table-list>

Due to testing complexity the page contains 500 `<table>` elements with single `<tr>` in each, and each `<tr>` contains 3 `<td>` cell elements.

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
            [FindSettings(Visibility = Visibility.Any)]
            public ControlList<ItemRow, _> Rows { get; private set; }

            public DataProvider<IEnumerable<int>, _> Ids
                => Rows.SelectContentsByExtraXPath<int>(ItemRow.XPathTo.Id, "Ids");

            public DataProvider<IEnumerable<string>, _> Names
                => Rows.SelectContentsByExtraXPath(ItemRow.XPathTo.Name, "Names");

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
                public const string Id = "td[contains(concat(' ', normalize-space(@class), ' '), ' id ')]";

                public const string Name = "td[contains(concat(' ', normalize-space(@class), ' '), ' name ')]";
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
        public void VerifyNoItemWithId_Fast()
        {
            Go.To<TableListPage>()
                .Items.Ids.Should.Not.Contain(999);
        }

        [Test]
        public void VerifyNoItemWithName_Fast()
        {
            Go.To<TableListPage>()
                .Items.Names.Should.Not.Contain("Unknown name");
        }

        [Test]
        public void VerifyItemWithName_Fast()
        {
            Go.To<TableListPage>()
                .Items.Names.Should.Contain("Item 250");
        }

        [Test]
        public void VerifyItemNameById_Fast()
        {
            Go.To<TableListPage>()
                .Items.FindRowById(250).Name.Should.Equal("Item 250");
        }

        [Test]
        public void VerifyItemByIdAndName_Fast()
        {
            Go.To<TableListPage>()
                .Items.FindRowByIdAndName(450, "Item 450").Should.BeVisible();
        }

        [Test]
        public void VerifyNoItemByIdAndName_Fast()
        {
            Go.To<TableListPage>()
                .Items.FindRowByIdAndName(999, "Item 999").Should.Not.BeVisible();
        }
    }
}
```