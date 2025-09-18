namespace AtataSamples.MultipleBrowsersViaRunSettings;

[TestFixture]
public abstract class UITestFixture
{
    [SetUp]
    public void SetUp() =>
        AtataContext.Configure().Build();

    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.Dispose();
}
