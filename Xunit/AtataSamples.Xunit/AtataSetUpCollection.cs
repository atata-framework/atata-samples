using Xunit;

namespace AtataSamples.Xunit;

[CollectionDefinition(Name)]
public class AtataSetUpCollection : ICollectionFixture<AtataSetUpFixture>
{
    public const string Name = "Atata set up";
}
