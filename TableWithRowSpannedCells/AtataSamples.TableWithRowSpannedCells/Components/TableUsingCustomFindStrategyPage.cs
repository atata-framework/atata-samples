using Atata;

namespace AtataSamples.TableWithRowSpannedCells;

using _ = TableUsingCustomFindStrategyPage;

[Url("table-with-row-spanned-cells")]
public class TableUsingCustomFindStrategyPage : Page<_>
{
    public Table<UserRow, _> Users { get; private set; }

    [FindSettings(
        Strategy = typeof(FindByColumnHeaderInTableWithRowSpannedCellsStrategy),
        TargetAttributeType = typeof(FindByColumnHeaderAttribute),
        TargetAnyType = true)]
    public class UserRow : TableRow<_>
    {
        public Text<_> Name { get; private set; }

        [Term(TermCase.Sentence)] // Uses sentence case as the column header is "Start date".
        [Format("yyyy-MM-dd")]
        public Date<_> StartDate { get; private set; }

        public Text<_> ExpertiseLevel { get; private set; }

        public Text<_> Client { get; private set; }

        public Text<_> Project { get; private set; }

        [Term(TermMatch.StartsWith)] // Column header is "Direct Project Cost (BGN)". StartsWith or Format can be used.
        public Number<_> DirectProjectCost { get; private set; }

        [Term(Format = "{0} (BGN)")] // Column header is "Gross Margin Percent (BGN)". StartsWith or Format can be used.
        public Number<_> GrossMarginPercent { get; private set; }
    }
}
