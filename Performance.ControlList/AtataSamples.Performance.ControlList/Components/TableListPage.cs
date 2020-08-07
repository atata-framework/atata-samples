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
            public ControlList<ItemRow, _> Rows { get; private set; }

            public DataProvider<IEnumerable<int>, _> Ids
                => Rows.SelectContentsByExtraXPath<int>($"/{ItemRow.XPathTo.Id}", "Ids");

            public DataProvider<IEnumerable<string>, _> Names
                => Rows.SelectContentsByExtraXPath($"/{ItemRow.XPathTo.Name}", "Names");

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
