namespace AtataSamples.DevExtreme;

using _ = SelectBoxPage;

[Url("/SelectBox/Overview/MaterialBlueLight/")]
public sealed class SelectBoxPage : Page<_>
{
    public enum Product
    {
        None,

        HDVideoPlayer,

        [Term("SuperHD Video Player")]
        SuperHDVideoPlayer,
    }

    [FindByIndex(0)]
    public DXSelectBox<_> StringBasedCombobox { get; private set; }

    [FindByIndex(0)]
    public DXSelectBox<Product?, _> EnumBasedCombobox { get; private set; }

    [FindByIndex(2)]
    public DXSelectBox<_> ReadOnlyCombobox { get; private set; }

    [FindByIndex(3)]
    public DXSelectBox<_> DisabledCombobox { get; private set; }

    protected override void OnInitCompleted()
    {
        base.OnInitCompleted();

        SwitchToFirstFrame();
    }

    private void SwitchToFirstFrame()
    {
        var frame = Find<Frame<_>>("Test");
        Driver.SwitchTo().Frame(frame.Scope);
    }
}
