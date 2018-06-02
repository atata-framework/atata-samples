using Atata;

namespace AtataSamples.ConfirmationPopups
{
    [Name("Deletion Confirmation")]
    [WindowTitle("Confirmation")]
    public class DeletionJQueryConfirmBox<TNavigateTo> : JQueryConfirmBox<DeletionJQueryConfirmBox<TNavigateTo>>
        where TNavigateTo : PageObject<TNavigateTo>
    {
        [Term(TermCase.MidSentence)]
        public ButtonDelegate<TNavigateTo, DeletionJQueryConfirmBox<TNavigateTo>> Delete { get; private set; }

        [Term(TermCase.MidSentence)]
        public ButtonDelegate<TNavigateTo, DeletionJQueryConfirmBox<TNavigateTo>> Cancel { get; private set; }
    }
}
