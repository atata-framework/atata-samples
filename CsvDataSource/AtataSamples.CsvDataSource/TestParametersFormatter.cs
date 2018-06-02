using System.Linq;

namespace AtataSamples.CsvDataSource
{
    public static class TestParametersFormatter
    {
        public static string Format(object value)
        {
            var propertyValues = value.GetType().GetProperties().Select(x => x.GetValue(value));
            var propertyValueStrings = propertyValues.Select(x =>
                x == null ? "null" :
                x is string ? $"\"{x}\"" :
                x);
            return string.Join(",", propertyValueStrings);
        }
    }
}
