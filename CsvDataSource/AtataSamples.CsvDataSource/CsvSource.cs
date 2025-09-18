using CsvHelper;

namespace AtataSamples.CsvDataSource;

public static class CsvSource
{
    public static TestCaseData[] Get<T>(string filePath, Type expectedResultType = null, string expectedResultName = "ExpectedResult")
    {
        string completeFilePath = Path.IsPathRooted(filePath)
            ? filePath
            : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

        using StreamReader streamReader = new(completeFilePath);
        using CsvReader csvReader = new(streamReader, Thread.CurrentThread.CurrentCulture);

        TestCaseData[] dataItems = [.. csvReader.GetRecords<T>().Select(x => new TestCaseData(x))];

        if (expectedResultType != null)
        {
            // Reset stream reader to beginning.
            streamReader.BaseStream.Position = 0;

            // Read the header line.
            csvReader.Read();

            object[] expectedResults = [.. GetExpectedResults(csvReader, expectedResultType, expectedResultName)];
            for (int i = 0; i < dataItems.Length; i++)
            {
                dataItems[i].Returns(expectedResults[i]);
            }
        }

        return dataItems;
    }

    private static IEnumerable<object> GetExpectedResults(CsvReader csvReader, Type expectedResultType, string expectedResultName)
    {
        while (csvReader.Read())
        {
            yield return csvReader.GetField(expectedResultType, expectedResultName);
        }
    }
}
