# [Atata Samples](https://github.com/atata-framework/atata-samples) / Authorization Role-Based Session Pools

[![Download sources](https://img.shields.io/badge/Download-sources-brightgreen.svg)](https://github.com/atata-framework/atata-samples/raw/main/_archives/AuthorizationRoleBasedSessionPools.zip)

Configures Atata to use a pool of authorization role-based web sessions.
Has ability to specify the required user-role session for each test method or test suite.

```cs
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
```

*[Download sources](https://github.com/atata-framework/atata-samples/raw/main/_archives/AuthorizationRoleBasedSessionPools.zip),
run tests, check results and experiment with [Atata Framework](https://atata.io).*
