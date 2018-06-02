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
                UseBaseUrl("https://atata-framework.github.io/atata-sample-app/#!/").
                UseCulture("en-us").
                UseTestName(testName).
                AddLogConsumer(new TestOutputLogConsumer(output)).
                Build();
        }

        public void Dispose()
        {
            AtataContext.Current.CleanUp();
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
