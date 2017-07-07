using Atata;
using Atata.Bootstrap;

namespace AtataSamples.ConfirmationPopups
{
    using _ = CommonConfirmationBSModal;

    public class CommonConfirmationBSModal : BSModal<_>
    {
        [FindByClass("btn-primary")]
        public ButtonDelegate<_> Confirm { get; private set; }

        [FindByClass("btn-default")]
        public ButtonDelegate<_> Cancel { get; private set; }
    }
}
