using Atata;

namespace AtataSamples.TableWithRowSpannedCells;

public class FindByRowSpannedCellIndexAttribute : FindByXPathAttribute
{
    public FindByRowSpannedCellIndexAttribute(int index)
        : base($"(self::*[td[@rowspan]] | preceding-sibling::tr[td[@rowspan]])[last()]/td[@rowspan]") =>
        Index = index;
}
