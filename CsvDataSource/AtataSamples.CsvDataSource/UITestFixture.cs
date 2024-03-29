﻿using Atata;
using NUnit.Framework;

namespace AtataSamples.CsvDataSource;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UITestFixture
{
    [SetUp]
    public void SetUp() =>
        AtataContext.Configure().Build();

    [TearDown]
    public void TearDown() =>
        AtataContext.Current?.Dispose();

    protected static UsersPage Login() =>
        Go.To<SignInPage>()
            .Email.Set("admin@mail.com")
            .Password.Set("abc123")
            .SignIn.ClickAndGo();
}
