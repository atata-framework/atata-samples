namespace AtataSamples.MSTest;

[TestClass]
public sealed class SampleTests : UITestFixture
{
    /// <summary>
    /// Simple test approach when you don't need to add exception/error information to the log.
    /// For example, when you use Visual Studio or CI system to view the log, as exception is displayed there any way.
    /// </summary>
    [TestMethod]
    public void MSTest() =>
        Go.To<HomePage>()
           .Header.Should.Be("Atata Sample App");

    /// <summary>
    /// Use such approach with <see cref="UITestFixture.Execute(System.Action)"/> method when you need to add exception/error information to the log.
    /// It is needed if you log to file or other external source.
    /// </summary>
    [TestMethod]
    public void MSTestWithExceptionLogging() =>
        Execute(() =>
            Go.To<HomePage>()
                .Header.Should.Be("Atata Sample App"));
}
