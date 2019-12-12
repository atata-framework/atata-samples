using Atata;
using OpenQA.Selenium.Remote;

namespace AtataSamples.ParallelTestsReusingDrivers
{
    public static class AtataContextBuilderExtensions
    {
        /// <summary>
        /// Use the driver pool with optionally resricting the pool with <paramref name="poolScopeObject"/>.
        /// </summary>
        /// <param name="builder">An <see cref="AtataContextBuilder"/>.</param>
        /// <param name="poolScopeObject">
        /// The pool scope object.
        /// Is optional.
        /// Mostly can be a fixture class object.
        /// When is <see langword="null"/> then will use whole global common pool;
        /// otherwise will use separate pool for particular scope object.
        /// </param>
        /// <returns></returns>
        public static AtataContextBuilder UseDriverPool(this AtataContextBuilder builder, object poolScopeObject = null)
        {
            IDriverFactory driverFactory = builder.BuildingContext.DriverFactoryToUse;

            RemoteWebDriver driver = DriverPool.Acquire(driverFactory, poolScopeObject);

            return builder.UseDriver(driver).
                OnCleanUp(ReleaseCurrentDriver);
        }

        private static void ReleaseCurrentDriver()
        {
            DriverPool.Release(AtataContext.Current?.Driver);
        }
    }
}
