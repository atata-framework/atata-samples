namespace AtataSamples.SalesforceLightning;

using _ = ComboboxPage;

[Url("lightning-combobox.html?type=Example")]
public sealed class ComboboxPage : Page<_>
{
    public enum Progress
    {
        New,
        InProgress,
        Finished
    }

    [FindByName("progress")]
    [CloseAlertBox(TriggerEvents.AfterSet)]
    public SLCombobox<_> StringBasedCombobox { get; private set; }

    [FindByName("progress")]
    [CloseAlertBox(TriggerEvents.AfterSet)]
    public SLCombobox<Progress, _> EnumBasedCombobox { get; private set; }

    [FindById("onetrust-accept-btn-handler")]
    public Button<_> AcceptAllCookies { get; private set; }

    protected override void OnInitCompleted()
    {
        AcceptAllCookies.Click();

        // Currently doesn't work. Before switching to iframe, there are few shadow DOM layers to go through.
        SwitchToFirstFrame();
    }

    private void SwitchToFirstFrame()
    {
        var frame = Controls.Create<Frame<_>>("Test", new FindByClassAttribute("playground-container"));
        Driver.SwitchTo().Frame(frame.Scope);
    }
}
