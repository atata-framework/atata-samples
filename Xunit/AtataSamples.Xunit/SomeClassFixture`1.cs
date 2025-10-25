﻿namespace AtataSamples.Xunit;

public sealed class SomeClassFixture<TClass> : AtataClassFixture<TClass>
{
    protected override void ConfigureSuiteAtataContext(AtataContextBuilder builder) =>
        builder.UseVariable(nameof(SomeClassFixture<TClass>), true); // Serves test purpose.
}
