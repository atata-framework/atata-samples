using Atata;

namespace AtataSamples.ConfirmationPopups
{
    public class ConfirmBSDeletionAttribute : TriggerAttribute
    {
        public ConfirmBSDeletionAttribute(TriggerEvents on = TriggerEvents.AfterClick, TriggerPriority priority = TriggerPriority.Medium)
            : base(on, priority)
        {
        }

        protected override void Execute<TOwner>(TriggerContext<TOwner> context)
        {
            Go.To<BSDeleteConfirmationModal<TOwner>>(temporarily: true).
                Delete();
        }
    }
}
