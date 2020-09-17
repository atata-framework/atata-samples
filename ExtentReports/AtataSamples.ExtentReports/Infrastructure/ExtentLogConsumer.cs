using System;
using AventStack.ExtentReports;

namespace Atata.ExtentReports
{
    public class ExtentLogConsumer : ILogConsumer
    {
        public void Log(LogEventInfo eventInfo)
        {
            Status status = ResolveLogStatus(eventInfo);

            string completeMessage = BuildCompleteMessage(eventInfo);
            completeMessage = NormalizeMessage(completeMessage);

            ExtentContext.CurrentTest.Log(status, completeMessage);
            ExtentContext.LastLogEventOfCurrentTest = eventInfo;
        }

        private static Status ResolveLogStatus(LogEventInfo eventInfo)
        {
            switch (eventInfo.Level)
            {
                case LogLevel.Trace:
                    return Status.Debug;
                case LogLevel.Debug:
                    return Status.Debug;
                case LogLevel.Info:
                    bool isEndOfVerificationLogSection = !(eventInfo.SectionEnd as VerificationLogSection)?.Message.StartsWith("Wait") ?? false;

                    if (isEndOfVerificationLogSection)
                    {
                        var lastLogLevel = ExtentContext.LastLogEventOfCurrentTest.Level;

                        if (lastLogLevel != LogLevel.Error && lastLogLevel != LogLevel.Warn)
                            return Status.Pass;
                    }

                    return Status.Info;
                case LogLevel.Warn:
                    return Status.Warning;
                case LogLevel.Error:
                    return Status.Fail;
                case LogLevel.Fatal:
                    return Status.Fatal;
                default:
                    throw ExceptionFactory.CreateForUnsupportedEnumValue(eventInfo.Level, $"{nameof(eventInfo)}.{nameof(LogEventInfo.Level)}");
            }
        }

        private static string BuildCompleteMessage(LogEventInfo eventInfo)
        {
            return !string.IsNullOrWhiteSpace(eventInfo.Message) && eventInfo.Exception != null
                ? $"{eventInfo.Message} {eventInfo.Exception}"
                : eventInfo.Exception != null
                ? eventInfo.Exception.ToString()
                : eventInfo.Message;
        }

        private static string NormalizeMessage(string message)
        {
            return message
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace(Environment.NewLine, "<br>")
                .Replace("<br>   ", "<br>&nbsp;&nbsp;&nbsp;")
                .Replace("<br>  ", "<br>&nbsp;&nbsp;");
        }
    }
}
