using Atata;

namespace AtataSamples.NetCore3.NUnit
{
    using _ = UsersPage;

    public class UsersPage : Page<_>
    {
        public H1<_> Heading { get; private set; }
    }
}
