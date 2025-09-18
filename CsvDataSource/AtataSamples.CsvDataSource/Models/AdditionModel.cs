namespace AtataSamples.CsvDataSource;

public sealed class AdditionModel
{
    public int Value1 { get; set; }

    public int Value2 { get; set; }

    public override string ToString() =>
        TestParametersFormatter.Format(this);
}
