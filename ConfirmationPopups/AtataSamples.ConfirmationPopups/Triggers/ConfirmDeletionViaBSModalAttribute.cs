using Atata;

namespace AtataSamples.ConfirmationPopups
{
    public class ConfirmDeletionViaBSModalAttribute : TriggerAttribute
    {
        public ConfirmDeletionViaBSModalAttribute(TriggerEvents on = TriggerEvents.AfterClick, TriggerPriority priority = TriggerPriority.Medium)
            : base(on, priority)
        {
        }

        protected override void Execute<TOwner>(TriggerContext<TOwner> context)
        {
            Go.To<DeletionConfirmationBSModal<TOwner>>(temporarily: true).
                Delete();
        }
    }
}
