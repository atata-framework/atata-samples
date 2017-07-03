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

            [Term("Delete Using BS Modal")]
            public ButtonDelegate<BSDeleteConfirmationModal<_>, _> DeleteUsingBSModal { get; private set; }

            [Term("Delete Using BS Modal")]
            [ConfirmBSDeletion]
            public ButtonDelegate<_> DeleteUsingBSModalViaTrigger { get; private set; }
        }
    }
}
