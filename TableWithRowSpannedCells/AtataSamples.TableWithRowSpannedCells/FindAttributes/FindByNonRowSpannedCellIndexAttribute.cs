namespace AtataSamples.TableWithRowSpannedCells;

public sealed class FindByNonRowSpannedCellIndexAttribute : FindByXPathAttribute
{
    public FindByNonRowSpannedCellIndexAttribute(int index)
        : base($"td[not(@rowspan)]") =>
        Index = index;
}
