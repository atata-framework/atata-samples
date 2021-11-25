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
    public class ExtentContext
    {
        private static readonly Lazy<string> s_workingDirectoryPath =
            new Lazy<string>(BuildWorkingDirectoryPath);

        private static readonly Lazy<ExtReports> s_lazyReports =
            new Lazy<ExtReports>(CreateAndInitReportsInstance);

        private static readonly ConcurrentDictionary<(string TestSuiteName, string TestName), ExtentContext> s_testExtentContextMap =
            new ConcurrentDictionary<(string TestSuiteName, string TestName), ExtentContext>();

        public ExtentContext(ExtentTest test)
        {
            Test = test;
        }

        public static string WorkingDirectoryPath => s_workingDirectoryPath.Value;

        public static string ReportTitle { get; set; } = "UI Tests Report";

        public static ExtReports Reports => s_lazyReports.Value;

        public ExtentTest Test { get; }

        public LogEventInfo LastLogEvent { get; set; }

        public static ExtentContext ResolveFor(AtataContext context)
        {
            string testSuiteName = context.TestSuiteName
                ?? throw new InvalidOperationException($"{nameof(AtataContext)}.{nameof(AtataContext.TestSuiteName)} is not set and cannot be used to create Extent test.");

            return s_testExtentContextMap.GetOrAdd((testSuiteName, context.TestName), StartExtentTest);
        }

        public static ExtentContext ResolveFor((string TestSuiteName, string TestName) testInfo) =>
            s_testExtentContextMap.GetOrAdd(testInfo, StartExtentTest);

        private static ExtentContext StartExtentTest((string TestSuiteName, string TestName) testInfo)
        {
            ExtentTest extentTest;

            if (string.IsNullOrEmpty(testInfo.TestName))
            {
                extentTest = Reports.CreateTest(testInfo.TestSuiteName);
            }
            else
            {
                var suiteContext = ResolveFor((testInfo.TestSuiteName, null));
                extentTest = suiteContext.Test.CreateNode(testInfo.TestName);
            }

            return new ExtentContext(extentTest);
        }

        private static ExtReports CreateAndInitReportsInstance()
        {
            string workingDirectoryPath = BuildWorkingDirectoryPath();
            ExtReports reports = new ExtReports();

            IEnumerable<IExtentReporter> reporters = CreateReporters(workingDirectoryPath);

            reports.AttachReporter(reporters.ToArray());

            return reports;
        }

        private static string BuildWorkingDirectoryPath() =>
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "artifacts",
                AtataContext.BuildStart.Value.ToString("yyyy-MM-dd HH_mm_ss"))
            + Path.DirectorySeparatorChar;

        private static IEnumerable<IExtentReporter> CreateReporters(string workingDirectoryPath)
        {
            var htmlReporter = new ExtentHtmlReporter(
                workingDirectoryPath.EndsWith(Path.DirectorySeparatorChar)
                ? workingDirectoryPath
                : workingDirectoryPath + Path.DirectorySeparatorChar);

            htmlReporter.Config.DocumentTitle = ReportTitle;

            yield return htmlReporter;
        }
    }
}
