using Atata;

namespace AtataSamples.ConfirmationPopups
{
    [PageObjectDefinition("div", ContainingClass = "jconfirm-box", ComponentTypeName = "confirm box")]
    [WindowTitleElementDefinition("span", ContainingClass = "jconfirm-title")]
    public class JQueryConfirmBox<TOwner> : PopupWindow<TOwner>
        where TOwner : JQueryConfirmBox<TOwner>
    {
    }
}
