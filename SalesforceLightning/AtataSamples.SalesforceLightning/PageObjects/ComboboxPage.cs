using Atata;

namespace AtataSamples.SalesforceLightning
{
    using _ = ComboboxPage;

    [Url("lightning:combobox/example")]
    public class ComboboxPage : Page<_>
    {
        [FindByName("progress")]
        [CloseAlertBox(TriggerEvents.AfterSet)]
        public SLCombobox<_> StringBasedCombobox { get; private set; }

        [FindByName("progress")]
        [CloseAlertBox(TriggerEvents.AfterSet)]
        public SLCombobox<Progress, _> EnumBasedCombobox { get; private set; }

        protected override void OnInitCompleted()
        {
            Find<Button<_>>(new FindByIdAttribute("onetrust-accept-btn-handler")).Click();
        }

        public enum Progress
        {
            New,
            InProgress,
            Finished
        }
    }
}
