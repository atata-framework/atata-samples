using System;
using System.Linq;
using System.Reflection;
using Atata;
using Xunit;
using Xunit.Abstractions;

namespace AtataSamples.Xunit
{
    [Collection(AtataSetUpCollection.Name)]
    public abstract class UITestFixture : IDisposable
    {
        protected UITestFixture(ITestOutputHelper output)
        {
            string testName = ResolveTestName(output);

            AtataContext.Configure()
                .UseTestName(testName)
                .LogConsumers.Add(new TextOutputLogConsumer(output.WriteLine))
                .Build();
        }

        public void Dispose() =>
            AtataContext.Current?.CleanUp();

        private static string ResolveTestName(ITestOutputHelper output)
        {
            ITest test = (ITest)output.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(x => x.FieldType == typeof(ITest))
                ?.GetValue(output);

            return test?.DisplayName;
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

            context.Log.Error(exception);

            try
            {
                context.TakeScreenshot("Failed");
            }
            catch (Exception screenshotException)
            {
                context.Log.Error("Take screenshot failed.", screenshotException);
            }
        }
    }
}
