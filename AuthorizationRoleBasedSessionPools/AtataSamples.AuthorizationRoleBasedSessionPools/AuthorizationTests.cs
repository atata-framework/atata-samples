namespace AtataSamples.AuthorizationRoleBasedSessionPools;

public sealed class AuthorizationTests : AtataTestSuite
{
    [Test]
    [TakeSessionFromPool(WebDriverSessionNames.UnauthorizedUserPool)]
    public void UsersPageIsForbiddenForUnauthorizedUser() =>
        Go.To<UsersPage>()
            .Header.Should.IgnoringCase.Contain("forbidden");

    [Test]
    [TakeSessionFromPool(WebDriverSessionNames.RegularUserPool)]
    public void UsersPageIsForbiddenForRegularUser() =>
        Go.To<UsersPage>()
            .Header.Should.IgnoringCase.Contain("forbidden");

    [Test]
    [TakeSessionFromPool(WebDriverSessionNames.AdminUserPool)]
    public void UsersPageIsAccessibleForAdminUser() =>
        Go.To<UsersPage>()
            .Header.Should.Be("Users");
}
