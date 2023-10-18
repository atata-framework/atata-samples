using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Atata;
using OpenQA.Selenium;

namespace AtataSamples.ParallelTestsReusingDrivers;

public static class DriverPool
{
    private static readonly ConcurrentBag<DriverEntry> s_globalEntries = new();

    private static readonly ConcurrentDictionary<object, ConcurrentBag<DriverEntry>> s_scopedEntries = new();

    private static IEnumerable<ConcurrentBag<DriverEntry>> AllEntryBags =>
        new[] { s_globalEntries }.Concat(s_scopedEntries.Values);

    private static IEnumerable<DriverEntry> AllEntries =>
        AllEntryBags.SelectMany(x => x);

    /// <summary>
    /// Acquires the driver for the specific <paramref name="driverFactory"/>.
    /// </summary>
    /// <param name="driverFactory">The driver factory.</param>
    /// <param name="poolScopeObject">
    /// The pool scope object.
    /// Is optional.
    /// Mostly can be a fixture class object.
    /// When is <see langword="null"/> then will use whole global common pool;
    /// otherwise will use separate pool for particular scope object.
    /// </param>
    /// <returns>An <see cref="IWebDriver"/> created or taken from pool.</returns>
    public static IWebDriver Acquire(IDriverFactory driverFactory, object poolScopeObject = null)
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

    private static ConcurrentBag<DriverEntry> ResolveEntriesBag(object poolScopeObject) =>
        poolScopeObject == null
            ? s_globalEntries
            : s_scopedEntries.GetOrAdd(poolScopeObject, _ => new ConcurrentBag<DriverEntry>());

    private static DriverEntry CreateDriverEntry(IDriverFactory driverFactory)
    {
        IWebDriver driver = driverFactory.Create();

        return new DriverEntry(driverFactory.Alias, driver);
    }

    public static void Release(IWebDriver driver)
    {
        DriverEntry entry = AllEntries.FirstOrDefault(x => x.Driver == driver);

        if (entry != null)
            entry.IsAcquired = false;
    }

    public static void DisposeAllForScope(object poolScopeObject)
    {
        if (s_scopedEntries.TryRemove(poolScopeObject, out var entries))
        {
            DisposeAndClear(entries);
        }
    }

    public static void DisposeAll()
    {
        foreach (var entries in AllEntryBags)
            DisposeAndClear(entries);

        s_scopedEntries.Clear();
    }

    private static void DisposeAndClear(ConcurrentBag<DriverEntry> entries)
    {
        foreach (DriverEntry entry in entries)
        {
            try
            {
                entry.Driver.Dispose();
            }
            catch
            {
                // Do nothing.
            }
        }

        entries.Clear();
    }

    private sealed class DriverEntry
    {
        public DriverEntry(string alias, IWebDriver driver)
        {
            Alias = alias;
            Driver = driver;
        }

        public string Alias { get; }

        public IWebDriver Driver { get; }

        public bool IsAcquired { get; set; }
    }
}
