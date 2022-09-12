using Atata;

namespace AtataSamples.TableWithRowSpannedCells
{
    public class FindByNonRowSpannedCellIndexAttribute : FindByXPathAttribute
    {
        public FindByNonRowSpannedCellIndexAttribute(int index)
            : base($"td[not(@rowspan)]") =>
            Index = index;
    }
}
