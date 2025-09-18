using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;

namespace Atata.ExtentReports;

public sealed class EndExtentTestItemEventHandler : IEventHandler<AtataContextDeInitCompletedEvent>
{
    public void Handle(AtataContextDeInitCompletedEvent eventData, AtataContext context)
    {
        ExtentContext extentContext = ExtentContext.ResolveFor(context);

        if (extentContext is not null)
        {
            extentContext.Test.Test.EndTime = DateTime.Now;
            extentContext.Test.Test.Status = ResolveCurrentTestStatus();
        }
    }

    // TODO: Atata vNext. Get status from AtataContext test info.
    private static Status ResolveCurrentTestStatus() =>
        TestContext.CurrentContext.Result.Outcome.Status switch
        {
            TestStatus.Inconclusive or TestStatus.Skipped => Status.Skip,
            TestStatus.Warning => Status.Warning,
            TestStatus.Failed => Status.Fail,
            _ => Status.Pass,
        };
}
