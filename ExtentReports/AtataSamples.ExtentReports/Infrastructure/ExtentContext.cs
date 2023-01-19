using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Reporter;
using ExtReports = AventStack.ExtentReports.ExtentReports;

namespace Atata.ExtentReports;

public class ExtentContext
{
    private static readonly Lazy<string> s_workingDirectoryPath =
        new(BuildWorkingDirectoryPath);

    private static readonly Lazy<ExtReports> s_lazyReports =
        new(CreateAndInitReportsInstance);

    private static readonly LockingConcurrentDictionary<string, ExtentContext> s_testSuiteExtentContextMap =
        new(StartExtentTestSuite);

    private static readonly LockingConcurrentDictionary<(string TestSuiteName, string TestName), ExtentContext> s_testExtentContextMap =
        new(StartExtentTest);

    private readonly object _nodeCreationLock = new();

    public ExtentContext(ExtentTest test) =>
        Test = test;

    public static string WorkingDirectoryPath => s_workingDirectoryPath.Value;

    public static string ReportTitle { get; set; } = "UI Tests Report";

    public static ExtReports Reports => s_lazyReports.Value;

    public ExtentTest Test { get; }

    public LogEventInfo LastLogEvent { get; set; }

    public static ExtentContext ResolveFor(AtataContext context)
    {
        string testSuiteName = context.TestSuiteName
            ?? throw new InvalidOperationException($"{nameof(AtataContext)}.{nameof(AtataContext.TestSuiteName)} is not set and cannot be used to create Extent test.");
        string testName = context.TestName;

        return testName is null
            ? ResolveForTestSuite(testSuiteName)
            : ResolveForTest(testSuiteName, testName);
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

        ExtentTest extentTest;

        lock (testSuiteContext._nodeCreationLock)
            extentTest = testSuiteContext.Test.CreateNode(testInfo.TestName);

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
            AtataContext.BuildStart.Value.ToString("yyyyMMddTHHmmss"))
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
