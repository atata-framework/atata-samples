using Atata;

namespace AtataSamples.JsonConfiguration.MultiEnvironment;

using _ = HomePage;

public class HomePage : Page<_>
{
    [FindById]
    public Link<SignInPage, _> SignIn { get; private set; }
}
