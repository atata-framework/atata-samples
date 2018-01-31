using Atata;

namespace AtataSamples.NetCore.NUnit
{
    using _ = UsersPage;

    public class UsersPage : Page<_>
    {
        public H1<_> Heading { get; private set; }
    }
}
