using System;
using System.Linq;
using System.Reflection;
using Atata;
using Xunit.Abstractions;

namespace AtataSamples.Xunit
{
    public class UITestFixture : IDisposable
    {
        public UITestFixture(ITestOutputHelper output)
        {
            string testName = ResolveTestName(output);

            AtataContext.Configure().
                UseChrome().
                    WithArguments("start-maximized").
                    WithLocalDriverPath().
                UseBaseUrl("https://demo.atata.io/").
                UseCulture("en-US").
                UseTestName(testName).
                AddLogConsumer(new TestOutputLogConsumer(output)).
                Build();
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

        public class TestOutputLogConsumer : TextOutputLogConsumer
        {
            private readonly ITestOutputHelper output;

            public TestOutputLogConsumer(ITestOutputHelper output)
            {
                this.output = output;
            }

            protected override void Write(string completeMessage)
            {
                output.WriteLine(completeMessage);
            }
        }
    }
}
