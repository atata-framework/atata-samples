using System;
using Atata;

namespace AtataSamples.JQueryUI;

public sealed class JQGridFindByColumnHeaderAttribute : FindByColumnHeaderAttribute
{
    protected override Type DefaultStrategy =>
        typeof(JQGridFindByColumnHeaderStrategy);
}
