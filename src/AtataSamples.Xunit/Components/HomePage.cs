using Atata;
using _ = AtataSamples.Xunit.HomePage;

namespace AtataSamples.Xunit
{
    public class HomePage : Page<_>
    {
        public H1<_> Header { get; private set; }
    }
}
