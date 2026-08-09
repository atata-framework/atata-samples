namespace AtataSamples.AspNetCore.UITests;

using _ = HomePage;

public sealed class HomePage : Page<_>
{
    [FindFirst]
    public H1<_> Header { get; private set; }
}
