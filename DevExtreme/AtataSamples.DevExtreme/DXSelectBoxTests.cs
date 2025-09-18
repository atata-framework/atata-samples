namespace AtataSamples.DevExtreme;

public class DXSelectBoxTests : UITestFixture
{
    [Test]
    [Explicit]
    public void AsString_GetAndSetValue()
    {
        var sut = Go.To<SelectBoxPage>().StringBasedCombobox;
        sut.Should.BeNull();

        sut.Set("ExcelRemote IP");
        sut.Should.Be("ExcelRemote IP");
    }

    [Test]
    [Explicit]
    public void AsEnum_GetAndSetValue()
    {
        var sut = Go.To<SelectBoxPage>().EnumBasedCombobox;
        sut.Should.BeNull();

        sut.Set(SelectBoxPage.Product.SuperHDVideoPlayer);
        sut.Should.Be(SelectBoxPage.Product.SuperHDVideoPlayer);
    }

    [Test]
    [Explicit]
    public void AsString_VerifyOptions()
    {
        var sut = Go.To<SelectBoxPage>().StringBasedCombobox;

        // Click select box to expand the drop-down list.
        sut.Click();

        // Verify some of the items.
        sut.DropDownList.Items.Should.Contain("SuperLED 50", "ExcelRemote IP");

        // Or verify the total number of items.
        sut.DropDownList.Items.Count.Should.Be(14);

        // Click once again to collapse the drop-down list, when needed.
        sut.Click();
    }

    [Test]
    [Explicit]
    public void ReadOnly() =>
        Go.To<SelectBoxPage>()
            .ReadOnlyCombobox.Should.BeReadOnly();

    [Test]
    [Explicit]
    public void Disabled() =>
        Go.To<SelectBoxPage>()
            .DisabledCombobox.Should.BeDisabled();
}
