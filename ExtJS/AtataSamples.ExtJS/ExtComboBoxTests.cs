﻿using Atata;
using NUnit.Framework;

namespace AtataSamples.ExtJS;

public class ExtComboBoxTests : UITestFixture
{
    [Test]
    public void ExtJS_ComboBox()
    {
        var control = Go.To<ExtComboBoxDemoPage>().State;

        // Set the value by typing its text:
        control.Set("Washington");
        control.Should.Equal("Washington");

        // Set the value by selecting it from drop-down:
        control.Select("Michigan");
        control.Should.Equal("Michigan");

        // Clear the value:
        control.Clear();
        control.Should.BeNullOrEmpty();

        // Verify the list of drop-down options using JavaScript:
        control.Options.Should.Contain(
            "Alabama", "California", "Colorado", "Florida", "Pennsylvania");

        // Verify the list of drop-down options visually:
        control.PickerTrigger.Click();
        control.DropDownList.Items.Contents.Should.Contain(
            "Alabama", "California", "Colorado", "Florida", "Pennsylvania");
    }
}
