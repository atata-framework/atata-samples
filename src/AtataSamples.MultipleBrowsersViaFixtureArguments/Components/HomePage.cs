using Atata;
using _ = AtataSamples.MultipleBrowsersViaFixtureArguments.HomePage;

namespace AtataSamples.MultipleBrowsersViaFixtureArguments
{
    public class HomePage : Page<_>
    {
        public H1<_> Header { get; private set; }
    }
}
