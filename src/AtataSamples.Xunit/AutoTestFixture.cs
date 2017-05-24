using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using Atata;
using Xunit.Abstractions;

namespace AtataSamples.Xunit
{
    public class AutoTestFixture : IDisposable
    {
        public AutoTestFixture(ITestOutputHelper output)
        {
            SetCulture("en-US");

            string testName = ResolveTestName(output);

            AtataContext.Build().
                UseChrome().
                UseBaseUrl("https://atata-framework.github.io/atata-sample-app/#!/").
                UseTestName(testName).
                AddLogConsumer(new TestOutputLogConsumer(output)).
                SetUp();
        }

        public void Dispose()
        {
            AtataContext.Current.CleanUp();
        }

        private void SetCulture(string cultureName)
        {
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private string ResolveTestName(ITestOutputHelper output)
        {
            ITest test = (ITest)output.GetType().
                GetFields(BindingFlags.NonPublic | BindingFlags.Instance).
                FirstOrDefault(x => x.FieldType == typeof(ITest))?.
                GetValue(output);

            return test?.DisplayName;
        }

        protected void Run(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception exception)
            {
                AtataContext.Current.Log.Error(null, exception);
                throw;
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
