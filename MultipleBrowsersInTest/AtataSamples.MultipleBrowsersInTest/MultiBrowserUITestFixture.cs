﻿using System.Collections.Generic;
using Atata;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AtataSamples.MultipleBrowsersInTest;

[TestFixture]
public class MultiBrowserUITestFixture
{
    protected List<AtataContext> Contexts { get; } = [];

    [SetUp]
    public void SetUp() =>
        CreateContext();

    protected AtataContext CreateContext()
    {
        AtataContextBuilder contextBuilder = AtataContext.Configure()
            .EventSubscriptions.Add<AtataContextDeInitCompletedEvent>(e => Contexts.Remove(e.Context));

        if (AtataContext.Current is not null)
            contextBuilder.UseTestName($"{AtataContext.Current.Test.Name}[{Contexts.Count}]");

        AtataContext context = contextBuilder.Build();

        Contexts.Add(context);

        return context;
    }

    protected void SwitchToContext(int indexOfContext) =>
        SwitchToContext(Contexts[indexOfContext]);

    protected void SwitchToContext(AtataContext context)
    {
        int currentContextIndex = Contexts.IndexOf(AtataContext.Current);
        AtataContext.Current.Log.Info($"Switching from context #{currentContextIndex}");

        context.SetAsCurrent();

        int targetContextIndex = Contexts.IndexOf(context);
        AtataContext.Current.Log.Info($"Switched to context #{targetContextIndex}");
    }

    [TearDown]
    public void TearDown()
    {
        foreach (var context in Contexts.ToArray())
        {
            context.Dispose();
        }
    }
}
