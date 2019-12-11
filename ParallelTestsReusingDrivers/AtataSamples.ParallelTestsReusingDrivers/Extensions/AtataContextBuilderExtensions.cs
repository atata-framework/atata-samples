using Atata;
using OpenQA.Selenium.Remote;

namespace AtataSamples.ParallelTestsReusingDrivers
{
    public static class AtataContextBuilderExtensions
    {
        public static AtataContextBuilder UseDriverPool(this AtataContextBuilder builder)
        {
            IDriverFactory driverFactory = builder.BuildingContext.DriverFactoryToUse;

            RemoteWebDriver driver = DriverPool.Acquire(driverFactory);

            return builder.UseDriver(driver);
        }
    }
}
