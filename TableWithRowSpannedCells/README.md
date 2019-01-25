# [Atata Samples](../) / Table with Row-Spanned Cells

[![Download sources](https://img.shields.io/badge/Download-Sources-brightgreen.svg)](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/atata-framework/atata-samples/tree/master/TableWithRowSpannedCells)

Demonstrates 3 different Atata approaches to work with table that has cells with `rowspan`.

*Download sources, run the tests, check exection, results and experiment with Atata.*

## Page Under Test

<https://atata-framework.github.io/atata-sample-app/#!/table-with-row-spanned-cells>

![Sample page](images/sample-page.png)

## Approach #1: Using FindByXPathAttribute

Uses power of XPath to find cell indices. But able to find only by indices, check Approach #3 to find cells by column header text.

### Page Object

*TableUsingXPathPage.cs*

```cs
using Atata;

namespace AtataSamples.TableWithRowSpannedCells
{
    using _ = TableUsingXPathPage;

    [Url("table-with-row-spanned-cells")]
    public class TableUsingXPathPage : Page<_>
    {
        public Table<UserRow, _> Users { get; private set; }

        public class UserRow : TableRow<_>
        {
            [FindByXPath(XPathTo.RowSpannedCell, Index = 0)]
            public Text<_> Name { get; private set; }

            [FindByXPath(XPathTo.RowSpannedCell, Index = 2)]
            [Format("yyyy-MM-dd")]
            public Date<_> StartDate { get; private set; }

            [FindByXPath(XPathTo.RowSpannedCell, Index = 8)]
            public Text<_> ExpertiseLevel { get; private set; }

            [FindByXPath(XPathTo.NonRowSpannedCell, Index = 0)]
            public Text<_> Client { get; private set; }

            [FindByXPath(XPathTo.NonRowSpannedCell, Index = 1)]
            public Text<_> Project { get; private set; }

            [FindByXPath(XPathTo.NonRowSpannedCell, Index = 16)]
            public Number<_> DirectProjectCost { get; private set; }

            [FindByXPath(XPathTo.RowSpannedCell, Index = 22)]
            public Number<_> GrossMarginPercent { get; private set; }

            private static class XPathTo
            {
                public const string RowSpannedCell = "(self::*[td[@rowspan]] | preceding-sibling::tr[td[@rowspan]])[last()]/td[@rowspan]";

                public const string NonRowSpannedCell = "td[not(@rowspan)]";
            }
        }
    }
}
```

## Approach #2: Using Custom Find Attributes

This approach is improvement of Approach #1.
It extracts XPath's to custom find attributes.
Extraction gives better usability of XPath search.

### Attributes

*FindByRowSpannedCellIndexAttribute.cs*
```cs
using Atata;

namespace AtataSamples.TableWithRowSpannedCells
{
    public class FindByRowSpannedCellIndexAttribute : FindByXPathAttribute
    {
        public FindByRowSpannedCellIndexAttribute(int index)
            : base($"(self::*[td[@rowspan]] | preceding-sibling::tr[td[@rowspan]])[last()]/td[@rowspan]")
        {
            Index = index;
        }
    }
}
```

*FindByNonRowSpannedCellIndexAttribute.cs*
```cs
using Atata;

namespace AtataSamples.TableWithRowSpannedCells
{
    public class FindByNonRowSpannedCellIndexAttribute : FindByXPathAttribute
    {
        public FindByNonRowSpannedCellIndexAttribute(int index)
            : base($"td[not(@rowspan)]")
        {
            Index = index;
        }
    }
}
```

### Page Object

*TableUsingCustomFindAttributesPage.cs*

```cs
using Atata;

namespace AtataSamples.TableWithRowSpannedCells
{
    using _ = TableUsingCustomFindAttributesPage;

    [Url("table-with-row-spanned-cells")]
    public class TableUsingCustomFindAttributesPage : Page<_>
    {
        public Table<UserRow, _> Users { get; private set; }

        public class UserRow : TableRow<_>
        {
            [FindByRowSpannedCellIndex(0)]
            public Text<_> Name { get; private set; }

            [FindByRowSpannedCellIndex(2)]
            [Format("yyyy-MM-dd")]
            public Date<_> StartDate { get; private set; }

            [FindByRowSpannedCellIndex(8)]
            public Text<_> ExpertiseLevel { get; private set; }

            [FindByNonRowSpannedCellIndex(0)]
            public Text<_> Client { get; private set; }

            [FindByNonRowSpannedCellIndex(1)]
            public Text<_> Project { get; private set; }

            [FindByNonRowSpannedCellIndex(16)]
            public Number<_> DirectProjectCost { get; private set; }

            [FindByRowSpannedCellIndex(22)]
            public Number<_> GrossMarginPercent { get; private set; }
        }
    }
}
```

## Approach #3: Using Custom Find Strategy

Gives ability to find cells by column header text contents.
By using custom strategy it is possible to configure finding of elements in any custom way.

### Strategy

*FindByColumnHeaderInTableWithRowSpannedCellsStrategy.cs*

```cs
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Atata;
using OpenQA.Selenium;

namespace AtataSamples.TableWithRowSpannedCells
{
    public class FindByColumnHeaderInTableWithRowSpannedCellsStrategy : IComponentScopeLocateStrategy
    {
        private const string HeaderXPath = "(ancestor::table)[position() = last()]//th";

        private const string BodyFirstRowCellsXPath = "(ancestor::table)[position() = last()]/tbody/tr[1]/td";

        private static readonly ConcurrentDictionary<Type, List<ColumnInfo>> TableColumnsInfoCache =
            new ConcurrentDictionary<Type, List<ColumnInfo>>();

        public ComponentScopeLocateResult Find(IWebElement scope, ComponentScopeLocateOptions options, SearchOptions searchOptions)
        {
            string xPath = BuildXPath(scope, options, searchOptions);

            if (xPath == null)
            {
                if (searchOptions.IsSafely)
                    return new MissingComponentScopeLocateResult();
                else
                    throw ExceptionFactory.CreateForNoSuchElement(options.GetTermsAsString(), searchContext: scope);
            }

            ComponentScopeLocateOptions xPathOptions = options.Clone();
            xPathOptions.Index = 0;
            xPathOptions.Terms = new string[] { xPath };

            return new FindByXPathStrategy().Find(scope, xPathOptions, searchOptions);
        }

        private string BuildXPath(IWebElement scope, ComponentScopeLocateOptions options, SearchOptions searchOptions)
        {
            List<ColumnInfo> columns = TableColumnsInfoCache.GetOrAdd(
                options.Metadata.ParentComponentType,
                _ => GetColumnInfoItems(scope, searchOptions));

            ColumnInfo column = columns.
                Where(x => options.Match.IsMatch(x.HeaderName, options.Terms)).
                ElementAtOrDefault(options.Index ?? 0);

            return column != null ? BuildXPathForCell(column, columns) : null;
        }

        protected virtual string BuildXPathForCell(ColumnInfo column, List<ColumnInfo> columns)
        {
            if (column.HasRowSpan)
            {
                int columnIndex = columns.IndexOf(column);
                return $"(self::*[td[1][@rowspan]] | preceding-sibling::tr[td[1][@rowspan]])[last()]/td[{columnIndex + 1}]";
            }
            else
            {
                int countOfPrecedingColumnsWithoutRowSpan = columns.TakeWhile(x => x != column).Count(x => !x.HasRowSpan);
                return $"td[not(@rowspan)][{countOfPrecedingColumnsWithoutRowSpan + 1}]";
            }
        }

        protected virtual List<ColumnInfo> GetColumnInfoItems(IWebElement scope, SearchOptions searchOptions)
        {
            var headers = scope.GetAll(By.XPath(HeaderXPath).With(searchOptions).OfAnyVisibility());

            var cells = scope.GetAll(By.XPath(BodyFirstRowCellsXPath).With(searchOptions).OfAnyVisibility());

            return headers.Select((header, index) =>
                new ColumnInfo
                {
                    HeaderName = header.Text,
                    HasRowSpan = cells.ElementAtOrDefault(index)?.GetAttribute("rowspan") != null
                }).ToList();
        }

        protected class ColumnInfo
        {
            public string HeaderName { get; set; }

            public bool HasRowSpan { get; set; }
        }
    }
}
```

### Page Object

*TableUsingCustomFindStrategyPage.cs*

```cs
using Atata;

namespace AtataSamples.TableWithRowSpannedCells
{
    using _ = TableUsingCustomFindStrategyPage;

    [Url("table-with-row-spanned-cells")]
    public class TableUsingCustomFindStrategyPage : Page<_>
    {
        public Table<UserRow, _> Users { get; private set; }

        [FindSettings(Strategy = typeof(FindByColumnHeaderInTableWithRowSpannedCellsStrategy),
            TargetAttributeType = typeof(FindByColumnHeaderAttribute),
            TargetAnyType = true)]
        public class UserRow : TableRow<_>
        {
            public Text<_> Name { get; private set; }

            [Term(TermCase.Sentence)] // Uses sentence case as the column header is "Start date".
            [Format("yyyy-MM-dd")]
            public Date<_> StartDate { get; private set; }

            public Text<_> ExpertiseLevel { get; private set; }

            public Text<_> Client { get; private set; }

            public Text<_> Project { get; private set; }

            [Term(TermMatch.StartsWith)] // Column header is "Direct Project Cost (BGN)". StartsWith or Format can be used.
            public Number<_> DirectProjectCost { get; private set; }

            [Term(Format = "{0} (BGN)")] // Column header is "Gross Margin Percent (BGN)". StartsWith or Format can be used.
            public Number<_> GrossMarginPercent { get; private set; }
        }
    }
}
```

## Test

Testing code for all approaches is the same as follows.

```cs
Go.To<TableUsingXPathPage>().
    Users.Rows.Should.HaveCount(3).

    Users.Rows[0].Name.Should.Equal("John Smith").
    Users.Rows[1].Name.Should.Equal("John Smith").
    Users.Rows[2].Name.Should.Equal("Total").

    Users.Rows[0].StartDate.Should.Equal(new DateTime(2016, 7, 7)).
    Users.Rows[1].StartDate.Should.BeGreater(new DateTime(2016, 1, 1)).
    Users.Rows[2].StartDate.Should.BeNull().

    Users.Rows[0].ExpertiseLevel.Should.Equal("Architect").
    Users.Rows[1].ExpertiseLevel.Should.Equal("Architect").
    Users.Rows[2].ExpertiseLevel.Should.BeNull().

    Users.Rows[0].Client.Should.Equal("SomeSoft").
    Users.Rows[1].Client.Should.Equal("Unassigned").
    Users.Rows[2].Client.Should.BeNull().

    Users.Rows[0].Project.Should.Equal("BioFruit").
    Users.Rows[1].Project.Should.Equal("Unassigned").
    Users.Rows[2].Project.Should.BeNull().

    Users.Rows.SelectData(x => x.DirectProjectCost).Should.EqualSequence(1693.42m, 564.47m, 2257.89m).

    Users.Rows[x => x.Name == "John Smith" && x.Client == "Unassigned" && x.Project == "Unassigned"].Should.Exist().
    Users.Rows[x => x.Name == "John Smith" && x.Client == "SomeSoft"].Project.Should.Equal("BioFruit").
    Users.Rows[x => x.Name == "Total"].GrossMarginPercent.Should.Equal(0.36m);
```

Test for each approach takes about 6 seconds to execute.

![Test results](images/test-results.png)