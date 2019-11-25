using Atata;

namespace AtataSamples.Specflow.Components
{
    using _ = HomePage;

    public class HomePage : BasePage<_>
    {
        public H1<_> Header { get; private set; }
    }
}
