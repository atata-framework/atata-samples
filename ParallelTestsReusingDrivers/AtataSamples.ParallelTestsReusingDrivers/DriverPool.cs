using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Atata;
using OpenQA.Selenium.Remote;

namespace AtataSamples.ParallelTestsReusingDrivers
{
    public static class DriverPool
    {
        private static readonly ConcurrentBag<DriverEntry> GlobalEntries = new ConcurrentBag<DriverEntry>();

        private static readonly ConcurrentDictionary<object, ConcurrentBag<DriverEntry>> ScopedEntries = new ConcurrentDictionary<object, ConcurrentBag<DriverEntry>>();

        private static IEnumerable<ConcurrentBag<DriverEntry>> AllEntryBags =>
            new[] { GlobalEntries }.Concat(ScopedEntries.Values);

        private static IEnumerable<DriverEntry> AllEntries =>
            AllEntryBags.SelectMany(x => x);

        /// <summary>
        /// Acquires the driver for the speific <paramref name="driverFactory"/>.
        /// </summary>
        /// <param name="driverFactory">The driver factory.</param>
        /// <param name="poolScopeObject">
        /// The pool scope object.
        /// Is optional.
        /// Mostly can be a fixture class object.
        /// When is <see langword="null"/> then will use whole global common pool;
        /// otherwise will use separate pool for particular scope object.
        /// </param>
        /// <returns>A <see cref="RemoteWebDriver"/> created or gotten from pool.</returns>
        /// <exception cref="ArgumentNullException">driverFactory</exception>
        public static RemoteWebDriver Acquire(IDriverFactory driverFactory, object poolScopeObject = null)
        {
            if (driverFactory == null)
                throw new ArgumentNullException(nameof(driverFactory));

            ConcurrentBag<DriverEntry> entries = ResolveEntriesBag(poolScopeObject);

            Monitor.Enter(entries);

            try
            {
                DriverEntry entry = entries.FirstOrDefault(x => x.Alias == driverFactory.Alias && !x.IsAcquired);

                if (entry is null)
                {
                    Monitor.Exit(entries);

                    entry = CreateDriverEntry(driverFactory);
                    entry.IsAcquired = true;

                    entries.Add(entry);
                }
                else
                {
                    entry.IsAcquired = true;
                }

                return entry.Driver;
            }
            finally
            {
                if (Monitor.IsEntered(entries))
                    Monitor.Exit(entries);
            }
        }

        private static ConcurrentBag<DriverEntry> ResolveEntriesBag(object poolScopeObject)
        {
            return poolScopeObject == null
                ? GlobalEntries
                : ScopedEntries.GetOrAdd(poolScopeObject, _ => new ConcurrentBag<DriverEntry>());
        }

        private static DriverEntry CreateDriverEntry(IDriverFactory driverFactory)
        {
            RemoteWebDriver driver = driverFactory.Create();

            return new DriverEntry(driverFactory.Alias, driver);
        }

        public static void Release(RemoteWebDriver driver)
        {
            DriverEntry entry = AllEntries.FirstOrDefault(x => x.Driver == driver);

            if (entry != null)
                entry.IsAcquired = false;
        }

        public static void CloseAllForScope(object poolScopeObject)
        {
            if (ScopedEntries.TryRemove(poolScopeObject, out var entries))
            {
                Close(entries);
            }
        }

        public static void CloseAll()
        {
            foreach (var entries in AllEntryBags)
                Close(entries);

            ScopedEntries.Clear();
        }

        private static void Close(ConcurrentBag<DriverEntry> entries)
        {
            foreach (DriverEntry entry in entries)
            {
                try
                {
                    entry.Driver.Dispose();
                }
                catch
                {
                }
            }

            entries.Clear();
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
