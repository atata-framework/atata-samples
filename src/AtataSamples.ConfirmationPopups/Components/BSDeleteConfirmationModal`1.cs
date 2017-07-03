using Atata;
using Atata.Bootstrap;

namespace AtataSamples.ConfirmationPopups
{
    [WindowTitle("Confirmation")]
    public class BSDeleteConfirmationModal<TNavigateTo> : BSModal<BSDeleteConfirmationModal<TNavigateTo>>
        where TNavigateTo : PageObject<TNavigateTo>
    {
        public ButtonDelegate<TNavigateTo, BSDeleteConfirmationModal<TNavigateTo>> Delete { get; private set; }

        public ButtonDelegate<TNavigateTo, BSDeleteConfirmationModal<TNavigateTo>> Cancel { get; private set; }
    }
}
