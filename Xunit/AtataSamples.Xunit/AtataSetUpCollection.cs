namespace AtataSamples.Xunit;

[CollectionDefinition(Name)]
public sealed class AtataSetUpCollection : ICollectionFixture<AtataSetUpFixture>
{
    public const string Name = "Atata set up";
}
