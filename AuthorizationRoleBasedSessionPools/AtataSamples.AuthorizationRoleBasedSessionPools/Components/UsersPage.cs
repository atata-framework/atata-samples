namespace AtataSamples.AuthorizationRoleBasedSessionPools;

using _ = UsersPage;

[Url("/users")]
public sealed class UsersPage : Page<_>
{
    public H1<_> Header { get; private set; }
}
