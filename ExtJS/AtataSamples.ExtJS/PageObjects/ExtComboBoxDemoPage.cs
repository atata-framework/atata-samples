namespace AtataSamples.ExtJS;

using _ = ExtComboBoxDemoPage;

[Url("https://examples.sencha.com/extjs/7.1.0/examples/kitchensink/#simple-combo")]
public sealed class ExtComboBoxDemoPage : PageObject<_>
{
    [FindByLabel("Select State:")]
    public ExtComboBox<_> State { get; private set; }

    protected override void OnInitCompleted()
    {
        base.OnInitCompleted();

        SwitchToFirstFrame();
    }

    private void SwitchToFirstFrame()
    {
        var frame = Controls.Create<Frame<_>>("Test");
        Driver.SwitchTo().Frame(frame.Scope);
    }
}
