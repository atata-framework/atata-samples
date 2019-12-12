using System;
using System.Collections.Concurrent;
using System.Linq;
using Atata;
using OpenQA.Selenium.Remote;

namespace AtataSamples.ParallelTestsReusingDrivers
{
    public static class DriverPool
    {
        private static readonly ConcurrentBag<DriverEntry> Entries = new ConcurrentBag<DriverEntry>();

        public static RemoteWebDriver Acquire(IDriverFactory driverFactory)
        {
            if (driverFactory == null)
                throw new ArgumentNullException(nameof(driverFactory));

            DriverEntry entry = Entries.FirstOrDefault(x => x.Alias == driverFactory.Alias && !x.IsAcquired);

            if (entry == null)
            {
                entry = CreateDriverEntry(driverFactory);
                Entries.Add(entry);
            }

            entry.IsAcquired = true;
            return entry.Driver;
        }

        private static DriverEntry CreateDriverEntry(IDriverFactory driverFactory)
        {
            RemoteWebDriver driver = driverFactory.Create();

            return new DriverEntry(driverFactory.Alias, driver);
        }

        public static void Release(RemoteWebDriver driver)
        {
            DriverEntry entry = Entries.FirstOrDefault(x => x.Driver == driver);

            if (entry != null)
                entry.IsAcquired = false;
        }

        public static void CloseAll()
        {
            foreach (DriverEntry entry in Entries)
            {
                try
                {
                    entry.Driver.Dispose();
                }
                catch
                {
                }
            }

            Entries.Clear();
        }

        private class DriverEntry
        {
            public DriverEntry(string alias, RemoteWebDriver driver)
            {
                Alias = alias;
                Driver = driver;
            }

            public string Alias { get; }

            public RemoteWebDriver Driver { get; }

            public bool IsAcquired { get; set; }
        }
    }
}
