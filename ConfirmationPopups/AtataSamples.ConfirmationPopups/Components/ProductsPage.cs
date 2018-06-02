using Atata;

namespace AtataSamples.ConfirmationPopups
{
    using _ = ProductsPage;

    [Url("products")]
    public class ProductsPage : Page<_>
    {
        public Table<ProductTableRow, _> Products { get; private set; }

        public class ProductTableRow : TableRow<_>
        {
            public Text<_> Name { get; private set; }

            public Currency<_> Price { get; private set; }

            public Number<_> Amount { get; private set; }

            [CloseConfirmBox]
            public ButtonDelegate<_> DeleteUsingJSConfirm { get; private set; }

            [FindByContent("Delete Using BS Modal")]
            public ButtonDelegate<DeletionConfirmationBSModal<_>, _> DeleteUsingBSModal { get; private set; }

            [FindByContent("Delete Using BS Modal")]
            [ConfirmDeletionViaBSModal]
            public ButtonDelegate<_> DeleteUsingBSModalViaTrigger { get; private set; }

            [FindByContent("Delete Using jquery-confirm")]
            public ButtonDelegate<DeletionJQueryConfirmBox<_>, _> DeleteUsingJQueryConfirm { get; private set; }

            [FindByContent("Delete Using jquery-confirm")]
            [ConfirmDeletionViaJQueryConfirmBox]
            public ButtonDelegate<_> DeleteUsingJQueryConfirmViaTrigger { get; private set; }
        }
    }
}
