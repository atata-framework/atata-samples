using Xunit;

namespace AtataSamples.Xunit
{
    [CollectionDefinition("Atata set up")]
    public class AtataSetUpCollection : ICollectionFixture<AtataSetUpFixture>
    {
    }
}
