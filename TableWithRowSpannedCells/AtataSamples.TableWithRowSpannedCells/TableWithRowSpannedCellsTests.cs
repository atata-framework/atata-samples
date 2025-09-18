namespace AtataSamples.TableWithRowSpannedCells;

public class TableWithRowSpannedCellsTests : UITestFixture
{
    [Test]
    public void UsingXPath() =>
        Go.To<TableUsingXPathPage>()
            .Users.Rows.Should.HaveCount(3)

            .Users.Rows[0].Name.Should.Equal("John Smith")
            .Users.Rows[1].Name.Should.Equal("John Smith")
            .Users.Rows[2].Name.Should.Equal("Total")

            .Users.Rows[0].StartDate.Should.Equal(new DateTime(2016, 7, 7))
            .Users.Rows[1].StartDate.Should.BeGreater(new DateTime(2016, 1, 1))
            .Users.Rows[2].StartDate.Should.BeNull()

            .Users.Rows[0].ExpertiseLevel.Should.Equal("Architect")
            .Users.Rows[1].ExpertiseLevel.Should.Equal("Architect")
            .Users.Rows[2].ExpertiseLevel.Should.BeEmpty()

            .Users.Rows[0].Client.Should.Equal("SomeSoft")
            .Users.Rows[1].Client.Should.Equal("Unassigned")
            .Users.Rows[2].Client.Should.BeEmpty()

            .Users.Rows[0].Project.Should.Equal("BioFruit")
            .Users.Rows[1].Project.Should.Equal("Unassigned")
            .Users.Rows[2].Project.Should.BeEmpty()

            .Users.Rows.SelectData(x => x.DirectProjectCost).Should.EqualSequence(1693.42m, 564.47m, 2257.89m)

            .Users.Rows[x => x.Name == "John Smith" && x.Client == "Unassigned" && x.Project == "Unassigned"].Should.BePresent()
            .Users.Rows[x => x.Name == "John Smith" && x.Client == "SomeSoft"].Project.Should.Equal("BioFruit")
            .Users.Rows[x => x.Name == "Total"].GrossMarginPercent.Should.Equal(0.36m);

    [Test]
    public void UsingCustomFindAttributes() =>
        Go.To<TableUsingCustomFindAttributesPage>()
            .Users.Rows.Should.HaveCount(3)

            .Users.Rows[0].Name.Should.Equal("John Smith")
            .Users.Rows[1].Name.Should.Equal("John Smith")
            .Users.Rows[2].Name.Should.Equal("Total")

            .Users.Rows[0].StartDate.Should.Equal(new DateTime(2016, 7, 7))
            .Users.Rows[1].StartDate.Should.BeGreater(new DateTime(2016, 1, 1))
            .Users.Rows[2].StartDate.Should.BeNull()

            .Users.Rows[0].ExpertiseLevel.Should.Equal("Architect")
            .Users.Rows[1].ExpertiseLevel.Should.Equal("Architect")
            .Users.Rows[2].ExpertiseLevel.Should.BeEmpty()

            .Users.Rows[0].Client.Should.Equal("SomeSoft")
            .Users.Rows[1].Client.Should.Equal("Unassigned")
            .Users.Rows[2].Client.Should.BeEmpty()

            .Users.Rows[0].Project.Should.Equal("BioFruit")
            .Users.Rows[1].Project.Should.Equal("Unassigned")
            .Users.Rows[2].Project.Should.BeEmpty()

            .Users.Rows.SelectData(x => x.DirectProjectCost).Should.EqualSequence(1693.42m, 564.47m, 2257.89m)

            .Users.Rows[x => x.Name == "John Smith" && x.Client == "Unassigned" && x.Project == "Unassigned"].Should.BePresent()
            .Users.Rows[x => x.Name == "John Smith" && x.Client == "SomeSoft"].Project.Should.Equal("BioFruit")
            .Users.Rows[x => x.Name == "Total"].GrossMarginPercent.Should.Equal(0.36m);

    [Test]
    public void UsingCustomFindStrategy() =>
        Go.To<TableUsingCustomFindStrategyPage>()
            .Users.Rows.Should.HaveCount(3)

            .Users.Rows[0].Name.Should.Equal("John Smith")
            .Users.Rows[1].Name.Should.Equal("John Smith")
            .Users.Rows[2].Name.Should.Equal("Total")

            .Users.Rows[0].StartDate.Should.Equal(new DateTime(2016, 7, 7))
            .Users.Rows[1].StartDate.Should.BeGreater(new DateTime(2016, 1, 1))
            .Users.Rows[2].StartDate.Should.BeNull()

            .Users.Rows[0].ExpertiseLevel.Should.Equal("Architect")
            .Users.Rows[1].ExpertiseLevel.Should.Equal("Architect")
            .Users.Rows[2].ExpertiseLevel.Should.BeEmpty()

            .Users.Rows[0].Client.Should.Equal("SomeSoft")
            .Users.Rows[1].Client.Should.Equal("Unassigned")
            .Users.Rows[2].Client.Should.BeEmpty()

            .Users.Rows[0].Project.Should.Equal("BioFruit")
            .Users.Rows[1].Project.Should.Equal("Unassigned")
            .Users.Rows[2].Project.Should.BeEmpty()

            .Users.Rows.SelectData(x => x.DirectProjectCost).Should.EqualSequence(1693.42m, 564.47m, 2257.89m)

            .Users.Rows[x => x.Name == "John Smith" && x.Client == "Unassigned" && x.Project == "Unassigned"].Should.BePresent()
            .Users.Rows[x => x.Name == "John Smith" && x.Client == "SomeSoft"].Project.Should.Equal("BioFruit")
            .Users.Rows[x => x.Name == "Total"].GrossMarginPercent.Should.Equal(0.36m);
}
