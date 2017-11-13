using System.Linq;

namespace AtataSamples.CsvDataSource
{
    public class UserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Office Office { get; set; }

        public Gender Gender { get; set; }

        public override string ToString()
        {
            var propertyValues = GetType().GetProperties().Select(x => x.GetValue(this));
            var propertyValueStrings = propertyValues.Select(x => x is string ? $"\"{x}\"" : x);
            return string.Join(",", propertyValueStrings);
        }
    }
}
