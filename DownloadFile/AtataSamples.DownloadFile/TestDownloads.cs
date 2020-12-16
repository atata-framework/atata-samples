using System;
using System.IO;
using Atata;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace AtataSamples.DownloadFile
{
    public static class TestDownloads
    {
        public static string DirectoryPath =>
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Downloads",
                GetCurrentTestFixtureName().SanitizeForFileName(),
                AtataContext.Current?.TestNameSanitized);

        public static void DeleteDirectory()
        {
            string directoryPath = DirectoryPath;

            if (Directory.Exists(directoryPath))
                Directory.Delete(directoryPath, true);
        }

        public static void ShouldContain(string fileName, double secondsToWait = 15d)
        {
            AtataContext.Current.Log.ExecuteSection(
                new LogSection($@"Assert: ""{fileName}"" file exists"),
                () =>
                {
                    string filePath = Path.Combine(DirectoryPath, fileName);

                    bool exists = new SafeWait<string>(filePath)
                    {
                        Timeout = TimeSpan.FromSeconds(secondsToWait)
                    }.Until(File.Exists);

                    Assert.That(exists, Is.True, $@"""{fileName}"" file should be downloaded.");
                });
        }

        private static string GetCurrentTestFixtureName()
        {
            string res = ResolveTestFixture(TestExecutionContext.CurrentContext.CurrentTest).Name;
            return res;
        }

        private static TestFixture ResolveTestFixture(ITest test)
        {
            if (test is null)
                throw new ArgumentNullException(nameof(test));

            return test as TestFixture ?? ResolveTestFixture(test.Parent);
        }
    }
}
