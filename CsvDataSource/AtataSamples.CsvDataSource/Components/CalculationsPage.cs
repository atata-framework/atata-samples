using Atata;

namespace AtataSamples.CsvDataSource
{
    using _ = CalculationsPage;

    [Url("calculations")]
    public class CalculationsPage : Page<_>
    {
        [FindById]
        public Input<int, _> AdditionValue1 { get; private set; }

        [FindById]
        public Input<int, _> AdditionValue2 { get; private set; }

        [FindById]
        public Input<int, _> AdditionResult { get; private set; }
    }
}
