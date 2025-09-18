namespace AtataSamples.TableWithRowSpannedCells;

using _ = TableUsingCustomFindAttributesPage;

[Url("table-with-row-spanned-cells")]
public sealed class TableUsingCustomFindAttributesPage : Page<_>
{
    public Table<UserRow, _> Users { get; private set; }

    public sealed class UserRow : TableRow<_>
    {
        [FindByRowSpannedCellIndex(0)]
        public Text<_> Name { get; private set; }

        [FindByRowSpannedCellIndex(2)]
        [Format("yyyy-MM-dd")]
        public Date<_> StartDate { get; private set; }

        [FindByRowSpannedCellIndex(8)]
        public Text<_> ExpertiseLevel { get; private set; }

        [FindByNonRowSpannedCellIndex(0)]
        public Text<_> Client { get; private set; }

        [FindByNonRowSpannedCellIndex(1)]
        public Text<_> Project { get; private set; }

        [FindByNonRowSpannedCellIndex(16)]
        public Number<_> DirectProjectCost { get; private set; }

        [FindByRowSpannedCellIndex(22)]
        public Number<_> GrossMarginPercent { get; private set; }
    }
}
