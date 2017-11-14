namespace AtataSamples.CsvDataSource
{
    public class AdditionModel
    {
        public int Value1 { get; set; }

        public int Value2 { get; set; }

        public override string ToString()
        {
            return TestParametersFormatter.Format(this);
        }
    }
}
