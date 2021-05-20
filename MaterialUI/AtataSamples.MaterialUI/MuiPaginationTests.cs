using Atata;
using NUnit.Framework;

namespace AtataSamples.MaterialUI
{
    public class MuiPaginationTests : UITestFixture
    {
        [Test]
        public void ClickNext()
        {
            Go.To<PaginationPage>()
                .Pagination.Next.Click()
                .Pagination.SelectedPageNumber.Should.Equal(2);
        }

        [Test]
        public void ClickPrevious()
        {
            Go.To<PaginationPage>()
                .Pagination.Next.Click()
                .Pagination.Previous.Click()
                .Pagination.SelectedPageNumber.Should.Equal(1);
        }

        [Test]
        public void FindButtonByPageNumber()
        {
            Go.To<PaginationPage>()
                .Pagination.FindButtonByPageNumber(3).Click()
                .Pagination.SelectedPageNumber.Should.Equal(3);
        }
    }
}
