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
            return TestParametersFormatter.Format(this);
        }
    }
}
