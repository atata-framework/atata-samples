using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Reporter;
using ExtReports = AventStack.ExtentReports.ExtentReports;

namespace Atata.ExtentReports
{
    public static class ExtentContext
    {
        private static readonly Lazy<ExtReports> lazyReports =
            new Lazy<ExtReports>(CreateAndInitReportsInstance);

        private static readonly ConcurrentDictionary<AtataContext, ExtentTestData> extentTestDataMap =
            new ConcurrentDictionary<AtataContext, ExtentTestData>();

        public static string WorkingFolder { get; set; } =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Report");

        public static string ReportTitle { get; set; } =
            "UI Tests Report";

        public static ExtReports Reports =>
            lazyReports.Value;

        private static ExtentTestData CurrentTestData =>
            extentTestDataMap.GetOrAdd(AtataContext.Current, StartExtentTest);

        public static ExtentTest CurrentTest =>
            CurrentTestData.Test;

        public static LogEventInfo LastLogEventOfCurrentTest
        {
            get => CurrentTestData.LastLogEvent;
            set => CurrentTestData.LastLogEvent = value;
        }

        private static ExtReports CreateAndInitReportsInstance()
        {
            ExtReports reports = new ExtReports();

            if (Directory.Exists(WorkingFolder))
                Directory.Delete(WorkingFolder, true);

            IEnumerable<IExtentReporter> reporters = CreateReporters();

            reports.AttachReporter(reporters.ToArray());

            return reports;
        }

        private static IEnumerable<IExtentReporter> CreateReporters()
        {
            var htmlReporter = new ExtentHtmlReporter(
                WorkingFolder.EndsWith(Path.DirectorySeparatorChar)
                ? WorkingFolder
                : WorkingFolder + Path.DirectorySeparatorChar);

            htmlReporter.Config.DocumentTitle = ReportTitle;

            yield return htmlReporter;
        }

        private static ExtentTestData StartExtentTest(AtataContext atataContext)
        {
            return new ExtentTestData(
                Reports.CreateTest(atataContext.TestName));
        }

        private class ExtentTestData
        {
            public ExtentTestData(ExtentTest test)
            {
                Test = test;
            }

            public ExtentTest Test { get; }

            public LogEventInfo LastLogEvent { get; set; }
        }
    }
}
