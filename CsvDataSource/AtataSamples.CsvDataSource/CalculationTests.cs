using Atata;
using NUnit.Framework;

namespace AtataSamples.CsvDataSource
{
    public class CalculationTests : UITestFixture
    {
        public static TestCaseData[] AdditionModels =>
            CsvSource.Get<AdditionModel>("addition-models.csv", expectedResultType: typeof(int));

        [TestCaseSource(nameof(AdditionModels))]
        public int Calculation_Addition(AdditionModel model)
        {
            return Go.To<CalculationsPage>().
                AdditionValue1.Set(model.Value1).
                AdditionValue2.Set(model.Value2).
                AdditionResult.Value;
        }
    }
}
