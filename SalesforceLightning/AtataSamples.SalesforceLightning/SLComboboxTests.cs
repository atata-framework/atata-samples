using Atata;
using NUnit.Framework;

namespace AtataSamples.SalesforceLightning
{
    public class SLComboboxTests : UITestFixture
    {
        [Test]
        [Explicit]
        public void AsString_GetAndSetValue()
        {
            var sut = Go.To<ComboboxPage>().StringBasedCombobox;
            sut.Should.Be("In Progress");

            sut.Set("New");
            sut.Should.Be("New");
        }

        [Test]
        [Explicit]
        public void AsEnum_GetAndSetValue()
        {
            var sut = Go.To<ComboboxPage>().EnumBasedCombobox;
            sut.Should.Be(ComboboxPage.Progress.InProgress);

            sut.Set(ComboboxPage.Progress.Finished);
            sut.Should.Be(ComboboxPage.Progress.Finished);
        }
    }
}
