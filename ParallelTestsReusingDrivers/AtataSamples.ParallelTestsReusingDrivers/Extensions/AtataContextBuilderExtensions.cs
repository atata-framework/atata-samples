using Atata;
using OpenQA.Selenium;

namespace AtataSamples.ParallelTestsReusingDrivers;

public static class AtataContextBuilderExtensions
{
    /// <summary>
    /// Use the driver pool with optionally restricting the pool with <paramref name="poolScopeObject"/>.
    /// </summary>
    /// <param name="builder">An <see cref="AtataContextBuilder"/>.</param>
    /// <param name="poolScopeObject">
    /// The pool scope object.
    /// Is optional.
    /// Mostly can be a fixture class object.
    /// When is <see langword="null"/> then will use whole global common pool;
    /// otherwise will use separate pool for particular scope object.
    /// </param>
    /// <returns>The same <see cref="AtataContextBuilder"/> instance.</returns>
    public static AtataContextBuilder UseDriverPool(this AtataContextBuilder builder, object poolScopeObject = null)
    {
        IDriverFactory driverFactory = builder.BuildingContext.DriverFactoryToUse;

        IWebDriver driver = DriverPool.Acquire(driverFactory, poolScopeObject);

        return builder.UseDriver(driver)
            .UseDisposeDriver(false)
            .EventSubscriptions.Add<DriverDeInitEvent>(ReleaseCurrentDriver);
    }

    private static void ReleaseCurrentDriver(DriverDeInitEvent eventData) =>
        DriverPool.Release(eventData.Driver);
}
