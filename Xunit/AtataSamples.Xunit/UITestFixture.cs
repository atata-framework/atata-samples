namespace AtataSamples.Xunit;

[Collection(AtataSetUpCollection.Name)]
public abstract class UITestFixture : IDisposable
{
    protected UITestFixture(ITestOutputHelper output)
    {
        string testFullName = ResolveTestName(output);
        Type testSuiteType = GetType();
        string testName = testFullName.Replace(testSuiteType.FullName!, null).TrimStart('.');

        AtataContext.Configure()
            .UseTestName(testName)
            .UseTestSuiteType(testSuiteType)
            .LogConsumers.Add(new TextOutputLogConsumer(output.WriteLine))
            .Build();
    }

    public void Dispose() =>
        AtataContext.Current?.Dispose();

    private static string ResolveTestName(ITestOutputHelper output)
    {
        ITest test = (ITest)output.GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .FirstOrDefault(x => x.FieldType == typeof(ITest))!
            .GetValue(output)!;

        return test.DisplayName;
    }

    protected static void Execute(Action action)
    {
        try
        {
            action?.Invoke();
        }
        catch (Exception exception)
        {
            OnException(exception);
            throw;
        }
    }

    private static void OnException(Exception exception)
    {
        var context = AtataContext.Current;

        context.Log.Error(exception, null);

        context.TakeScreenshot("Failed");
        context.TakePageSnapshot("Failed");
    }
}
