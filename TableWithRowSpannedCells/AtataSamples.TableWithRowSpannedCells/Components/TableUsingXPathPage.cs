namespace AtataSamples.TableWithRowSpannedCells;

using _ = TableUsingXPathPage;

[Url("table-with-row-spanned-cells")]
public sealed class TableUsingXPathPage : Page<_>
{
    public Table<UserRow, _> Users { get; private set; }

    public sealed class UserRow : TableRow<_>
    {
        [FindByXPath(XPathTo.RowSpannedCell, Index = 0)]
        public Text<_> Name { get; private set; }

        [FindByXPath(XPathTo.RowSpannedCell, Index = 2)]
        [Format("yyyy-MM-dd")]
        public Date<_> StartDate { get; private set; }

        [FindByXPath(XPathTo.RowSpannedCell, Index = 8)]
        public Text<_> ExpertiseLevel { get; private set; }

        [FindByXPath(XPathTo.NonRowSpannedCell, Index = 0)]
        public Text<_> Client { get; private set; }

        [FindByXPath(XPathTo.NonRowSpannedCell, Index = 1)]
        public Text<_> Project { get; private set; }

        [FindByXPath(XPathTo.NonRowSpannedCell, Index = 16)]
        public Number<_> DirectProjectCost { get; private set; }

        [FindByXPath(XPathTo.RowSpannedCell, Index = 22)]
        public Number<_> GrossMarginPercent { get; private set; }

        private static class XPathTo
        {
            public const string RowSpannedCell = "(self::*[td[@rowspan]] | preceding-sibling::tr[td[@rowspan]])[last()]/td[@rowspan]";

            public const string NonRowSpannedCell = "td[not(@rowspan)]";
        }
    }
}
