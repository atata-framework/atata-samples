using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using AventStack.ExtentReports;

namespace Atata.ExtentReports;

public sealed class ExtentLogConsumer : ILogConsumer
{
    public void Log(LogEventInfo eventInfo)
    {
        if (ShouldLog(eventInfo))
        {
            Status status = ResolveLogStatus(eventInfo);

            string completeMessage = BuildCompleteMessage(eventInfo);
            completeMessage = NormalizeMessage(completeMessage);

            ExtentContext extentContext = ExtentContext.ResolveFor(eventInfo.Context);
            extentContext.Test.Log(status, completeMessage);
            extentContext.LastLogEvent = eventInfo;
        }
    }

    // Temporary workaround to skip messages that should and will have Trace/Debug log level.
    // After upgrade Atata to v3, remove this method.
    private static bool ShouldLog(LogEventInfo eventInfo) =>
        !eventInfo.Message.StartsWith("Take screenshot", StringComparison.Ordinal) &&
        !eventInfo.Message.StartsWith("Take page snapshot", StringComparison.Ordinal) &&
        !eventInfo.Message.StartsWith("Screenshot saved to file", StringComparison.Ordinal) &&
        !eventInfo.Message.StartsWith("Starting test", StringComparison.Ordinal) &&
        !eventInfo.Message.StartsWith("Finished test", StringComparison.Ordinal);

    private static Status ResolveLogStatus(LogEventInfo eventInfo)
    {
        switch (eventInfo.Level)
        {
            case LogLevel.Trace:
            case LogLevel.Debug:
                return 0;
            case LogLevel.Info:
                if (eventInfo.SectionEnd is VerificationLogSection)
                {
                    if (eventInfo.SectionEnd.Exception != null)
                    {
                        return Status.Fail;
                    }
                    else if (!eventInfo.SectionEnd.Message.StartsWith("Wait", StringComparison.Ordinal))
                    {
                        var lastLogLevel = ExtentContext.ResolveFor(eventInfo.Context)
                            .LastLogEvent.Level;

                        if (lastLogLevel is LogLevel.Error)
                            return Status.Fail;
                        else if (lastLogLevel is not LogLevel.Warn)
                            return Status.Pass;
                    }
                }

                return Status.Info;
            case LogLevel.Warn:
                return Status.Warning;
            case LogLevel.Error:
                return Status.Error;
            case LogLevel.Fatal:
                return Status.Error;
            default:
                throw ExceptionFactory.CreateForUnsupportedEnumValue(eventInfo.Level, $"{nameof(eventInfo)}.{nameof(LogEventInfo.Level)}");
        }
    }

    private static string BuildCompleteMessage(LogEventInfo eventInfo) =>
        !string.IsNullOrWhiteSpace(eventInfo.Message) && eventInfo.Exception != null
            ? $"{eventInfo.Message} {eventInfo.Exception}"
            : eventInfo.Exception != null
            ? eventInfo.Exception.ToString()
            : eventInfo.Message;

    private static string NormalizeMessage(string message)
    {
        message = HttpUtility.HtmlEncode(message)
            .Replace(Environment.NewLine, "<br>");

        return Regex.Replace(
            message,
            @"(?<=\<br\>)\s+",
            match => string.Concat(Enumerable.Repeat("&nbsp;", match.Length)));
    }
}
