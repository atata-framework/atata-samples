﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using AventStack.ExtentReports;

namespace Atata.ExtentReports;

public sealed partial class ExtentLogConsumer : ILogConsumer
{
    public void Log(LogEventInfo eventInfo)
    {
        Status status = ResolveLogStatus(eventInfo);

        string completeMessage = BuildCompleteMessage(eventInfo);
        completeMessage = NormalizeMessage(completeMessage);

        ExtentContext extentContext = ExtentContext.ResolveFor(eventInfo.Context);
        extentContext.Test.Log(status, completeMessage);
        extentContext.LastLogEvent = eventInfo;
    }

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

        return GetNormalizeMessageLineBreakRegex()
            .Replace(message, match => string.Concat(Enumerable.Repeat("&nbsp;", match.Length)));
    }

    [GeneratedRegex(@"(?<=\<br\>)\s+")]
    private static partial Regex GetNormalizeMessageLineBreakRegex();
}
