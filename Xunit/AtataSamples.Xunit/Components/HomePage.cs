﻿using Atata;

namespace AtataSamples.Xunit;

using _ = HomePage;

public class HomePage : Page<_>
{
    public H1<_> Header { get; private set; }
}
