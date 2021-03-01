using System;
using System.Linq;
using System.Reflection;
using Atata;
using Xunit;
using Xunit.Abstractions;

namespace AtataSamples.Xunit
{
    [Collection("Atata set up")]
    public abstract class UITestFixture : IDisposable
    {
        protected UITestFixture(ITestOutputHelper output)
        {
            string testName = ResolveTestName(output);

            AtataContext.Configure()
                .UseTestName(testName)
                .AddLogConsumer(new TextOutputLogConsumer(output.WriteLine))
                .Build();
        }

        public void Dispose()
        {
            AtataContext.Current?.CleanUp();
        }

        private string ResolveTestName(ITestOutputHelper output)
        {
            ITest test = (ITest)output.GetType().
                GetFields(BindingFlags.NonPublic | BindingFlags.Instance).
                FirstOrDefault(x => x.FieldType == typeof(ITest))?.
                GetValue(output);

            return test?.DisplayName;
        }

        protected void Execute(Action action)
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
            ILogManager log = AtataContext.Current.Log;

            log.Error(exception);

            try
            {
                log.Screenshot("Failed");
            }
            catch (Exception screenshotException)
            {
                log.Error("Take screenshot failed.", screenshotException);
            }
        }
    }
}
