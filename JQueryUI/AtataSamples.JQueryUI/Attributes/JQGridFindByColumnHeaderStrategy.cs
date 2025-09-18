namespace AtataSamples.JQueryUI;

public sealed class JQGridFindByColumnHeaderStrategy : FindByColumnHeaderStrategy
{
    public JQGridFindByColumnHeaderStrategy()
        : base("ancestor::*[contains(concat(' ', normalize-space(@class), ' '), ' ui-jqgrid-view ')][1]//table[contains(concat(' ', normalize-space(@class), ' '), ' ui-jqgrid-htable ')][1]//th")
    {
    }
}
