namespace AtataSamples.NUnit.GenericPageTests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class UITestFixture
{
    [SetUp]
    public void SetUp() =>
        AtataContext.Configure().Build();

    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.Dispose();
}
