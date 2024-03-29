﻿using Atata;

namespace AtataSamples.MaterialUI;

using _ = PaginationPage;

[Url("components/pagination/")]
public class PaginationPage : Page<_>
{
    [Name("Main")]
    public MuiPagination<_> Pagination { get; private set; }
}
