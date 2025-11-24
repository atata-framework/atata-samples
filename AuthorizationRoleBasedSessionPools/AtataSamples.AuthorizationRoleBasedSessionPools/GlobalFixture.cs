using System.Collections.Concurrent;

namespace AtataSamples.AuthorizationRoleBasedSessionPools;

public sealed class GlobalFixture : AtataGlobalFixture
{
    protected override void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder)
    {
        // Configure new sessions for unauthorized users
        // for cases when you might need to test authentication/authorization using UI
        // and don't want to return such session to a session pool.
        builder.Sessions.AddWebDriver(sessionBuilder =>
        {
            sessionBuilder.UseName(WebDriverSessionNames.UnauthorizedUserOwn);
            ConfigureWebDriverSession(sessionBuilder);
        });

        builder.LogConsumers.AddNLogFile();
    }

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder)
    {
        builder.SetUpWebDriversForUse();

        // Configure session pool for unauthorized users on global AtataContext.
        builder.Sessions.AddWebDriver(sessionBuilder =>
        {
            sessionBuilder.UseName(WebDriverSessionNames.UnauthorizedUserPool);
            sessionBuilder.UseAsPool();
            ConfigureWebDriverSession(sessionBuilder);
        });

        // Predefined user credentials.
        // Can be loaded from config file or other source.
        // Demo application actually doesn't support roles, so regular users will actually fail to login.
        // But for the sake of demo tests, we will assume they are working as intended.
        // Admin users will be able to login successfully.
        ConcurrentQueue<UserCredentials> regularUserCredentials = new([
            new("user1@mail.com", "abc123"),
            new("user2@mail.com", "abc123")]);

        ConcurrentQueue<UserCredentials> adminUserCredentials = new([
            new("admin@mail.com", "abc123"),
            new("jane.smith@mail.com", "abc123")]);

        // Configure session pool for regular users on global AtataContext.
        builder.Sessions.AddWebDriver(sessionBuilder =>
        {
            sessionBuilder.UseName(WebDriverSessionNames.RegularUserPool);
            ConfigureWebDriverSessionAuthorizedPool(sessionBuilder, regularUserCredentials);
        });

        // Configure session pool for admin users on global AtataContext.
        builder.Sessions.AddWebDriver(sessionBuilder =>
        {
            sessionBuilder.UseName(WebDriverSessionNames.AdminUserPool);
            ConfigureWebDriverSessionAuthorizedPool(sessionBuilder, adminUserCredentials);
        });
    }

    private static void ConfigureWebDriverSessionAuthorizedPool(
        WebDriverSessionBuilder sessionBuilder,
        ConcurrentQueue<UserCredentials> credentialsQueue)
    {
        sessionBuilder.UseAsPool(x => x
            .WithMaxCapacity(credentialsQueue.Count));

        ConfigureWebDriverSession(sessionBuilder);

        sessionBuilder.EventSubscriptions.Add<AtataSessionInitCompletedEvent>(eventData =>
        {
            if (!credentialsQueue.TryDequeue(out UserCredentials? credentials))
                throw new InvalidOperationException($"No more user credentials available for {sessionBuilder}.");

            eventData.Session.State.Set(credentials);
            ((WebDriverSession)eventData.Session).Login(credentials);
        });
    }

    private static void ConfigureWebDriverSession(WebDriverSessionBuilder sessionBuilder) =>
        sessionBuilder.UseChrome(x => x
            .WithArguments(
                "start-maximized",
                "disable-search-engine-choice-screen")
            .WithInitialHealthCheck())
            .UseBaseUrl("https://demo.atata.io/");
}
