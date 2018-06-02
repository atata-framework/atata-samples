using Atata;

namespace AtataSamples.ConfirmationPopups
{
    public class ConfirmDeletionViaJQueryConfirmBoxAttribute : TriggerAttribute
    {
        public ConfirmDeletionViaJQueryConfirmBoxAttribute(TriggerEvents on = TriggerEvents.AfterClick, TriggerPriority priority = TriggerPriority.Medium)
            : base(on, priority)
        {
        }

        protected override void Execute<TOwner>(TriggerContext<TOwner> context)
        {
            Go.To<DeletionJQueryConfirmBox<TOwner>>(temporarily: true).
                Delete();
        }
    }
}
