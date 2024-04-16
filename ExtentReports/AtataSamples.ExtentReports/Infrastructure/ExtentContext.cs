using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Listener.Entity;
using AventStack.ExtentReports.Reporter;
using ExtReports = AventStack.ExtentReports.ExtentReports;

namespace Atata.ExtentReports;

public sealed class ExtentContext
{
    private static readonly Lazy<string> s_workingDirectoryPath =
        new(BuildWorkingDirectoryPath);

    private static readonly Lazy<ExtReports> s_lazyReports =
        new(CreateAndInitReportsInstance);

    private static readonly LockingConcurrentDictionary<string, ExtentContext> s_testSuiteExtentContextMap =
        new(StartExtentTestSuite);

    private static readonly LockingConcurrentDictionary<(string TestSuiteName, string TestName), ExtentContext> s_testExtentContextMap =
        new(StartExtentTest);

    public ExtentContext(ExtentTest test) =>
        Test = test;

    public static string WorkingDirectoryPath => s_workingDirectoryPath.Value;

    public static string ReportTitle { get; set; } = "UI Tests Report";

    public static string ReportFileName { get; set; } = "Report.html";

    public static string AdditionalReportStyle { get; set; } =
@" <style>
  tr.event-row > td { vertical-align: top; font-family: ""Cascadia Mono"", Consolas, ""Courier New""; color: #222; line-height: 1.5 !important; }
  .table-sm > tbody > tr > td, .table-sm > thead > tr > th { padding: 0.2em; }
  .mb-3 { margin-bottom: 0 !important; }
  .mt-4 { margin-top: 0.5rem !important; }
  .detail-body img { padding: 0; border: 1px solid #ccc; }
  .artifacts { padding-left: 2em; }
 </style>
";

    public static ExtReports Reports => s_lazyReports.Value;

    public ExtentTest Test { get; }

    public LogEventInfo LastLogEvent { get; set; }

    public static ExtentContext ResolveFor(AtataContext context)
    {
        string testSuiteName = context.Test.SuiteName
            ?? throw new InvalidOperationException($"{nameof(AtataContext)}.{nameof(AtataContext.Test)}.{nameof(TestInfo.SuiteName)} is not set and cannot be used to create Extent test.");
        string testName = context.Test.Name;

        return testName is null
            ? ResolveForTestSuite(testSuiteName)
            : ResolveForTest(testSuiteName, testName);
    }

    public static void Flush()
    {
        Reports.Flush();

        AddStyleToHtmlReport();
    }

    private static void AddStyleToHtmlReport()
    {
        if (string.IsNullOrEmpty(AdditionalReportStyle))
            return;

        string reportFilePath = Path.Combine(WorkingDirectoryPath, ReportFileName);

        try
        {
            if (File.Exists(reportFilePath))
            {
                string reportContent = File.ReadAllText(reportFilePath);
                int headEndIndex = reportContent.IndexOf("</head>", StringComparison.Ordinal);

                if (headEndIndex >= 0)
                {
                    string updatedReportContent = reportContent.Insert(headEndIndex, AdditionalReportStyle);
                    File.WriteAllText(reportFilePath, updatedReportContent);
                }
            }
        }
        catch
        {
            // Do nothing. Not critical.
        }
    }

    private static ExtentContext ResolveForTestSuite(string testSuiteName) =>
        s_testSuiteExtentContextMap.GetOrAdd(testSuiteName);

    private static ExtentContext ResolveForTest(string testSuiteName, string testName) =>
        s_testExtentContextMap.GetOrAdd((testSuiteName, testName));

    private static ExtentContext StartExtentTestSuite(string testSuiteName)
    {
        ExtentTest extentTest = Reports.CreateTest(testSuiteName);

        return new ExtentContext(extentTest);
    }

    private static ExtentContext StartExtentTest((string TestSuiteName, string TestName) testInfo)
    {
        var testSuiteContext = ResolveForTestSuite(testInfo.TestSuiteName);

        ExtentTest extentTest = testSuiteContext.Test.CreateNode(testInfo.TestName);

        return new ExtentContext(extentTest);
    }

    private static ExtReports CreateAndInitReportsInstance()
    {
        ExtReports reports = new ExtReports();

        var reporters = CreateReporters(WorkingDirectoryPath);

        reports.AttachReporter(reporters.ToArray());

        return reports;
    }

    private static string BuildWorkingDirectoryPath() =>
        AtataContext.GlobalProperties.ArtifactsRootPath + Path.DirectorySeparatorChar;

    private static IEnumerable<IObserver<ReportEntity>> CreateReporters(string workingDirectoryPath)
    {
        var sparkReporter = new ExtentSparkReporter(
            Path.Combine(workingDirectoryPath, ReportFileName));

        sparkReporter.Config.DocumentTitle = $"{ReportTitle} / {AtataContext.GlobalProperties.BuildStart:yyyy-MM-dd HH:mm:ss}";

        yield return sparkReporter;
    }

    private sealed class LockingConcurrentDictionary<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, Lazy<TValue>> _dictionary;

        private readonly Func<TKey, Lazy<TValue>> _valueFactory;

        public LockingConcurrentDictionary(Func<TKey, TValue> valueFactory)
        {
            _dictionary = new ConcurrentDictionary<TKey, Lazy<TValue>>();
            _valueFactory = key => new Lazy<TValue>(() => valueFactory(key));
        }

        public TValue GetOrAdd(TKey key) =>
            _dictionary.GetOrAdd(key, _valueFactory).Value;
    }
}
