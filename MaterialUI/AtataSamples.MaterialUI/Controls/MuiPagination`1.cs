using Atata;

namespace AtataSamples.MaterialUI
{
    [ControlDefinition(ContainingClass = "MuiPagination-root", ComponentTypeName = "pagination")]
    public class MuiPagination<TOwner> : Control<TOwner>
        where TOwner : PageObject<TOwner>
    {
        [FindFirst]
        public Button<TOwner> Previous { get; private set; }

        [FindLast]
        public Button<TOwner> Next { get; private set; }

        public ControlList<Button<TOwner>, TOwner> Buttons { get; private set; }

        [FindByClass("Mui-selected")]
        public Button<TOwner> SelectedPageButton { get; private set; }

        public ValueProvider<int, TOwner> SelectedPageNumber =>
            CreateValueProvider("selected page number", () => int.Parse(SelectedPageButton.Content.Value));

        public Button<TOwner> FindButtonByPageNumber(int number)
        {
            string numberAsString = number.ToString();
            return Controls.CreateButton(numberAsString, new FindByContentAttribute(numberAsString));
        }
    }
}
