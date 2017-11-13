using System.Linq;

namespace AtataSamples.CsvDataSource
{
    public class AdditionModel
    {
        public int Value1 { get; set; }

        public int Value2 { get; set; }

        public override string ToString()
        {
            var propertyValues = GetType().GetProperties().Select(x => x.GetValue(this));
            var propertyValueStrings = propertyValues.Select(x => x is string ? $"\"{x}\"" : x);
            return string.Join(",", propertyValueStrings);
        }
    }
}
