using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Atata;
using OpenQA.Selenium;

namespace AtataSamples.TableWithRowSpannedCells
{
    public class FindByColumnHeaderInTableWithRowSpannedCellsStrategy : IComponentScopeLocateStrategy
    {
        private const string HeaderXPath = "(ancestor::table)[position() = last()]//th";

        private static readonly ConcurrentDictionary<Type, List<ColumnInfo>> TableColumnsInfoCache =
            new ConcurrentDictionary<Type, List<ColumnInfo>>();

        public string RowWithSpannedCellsXPathCondition { get; set; } = "td[@rowspan and normalize-space(@rowspan) != '1']";

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
                _ => GetColumnInfoItems(scope));

            ColumnInfo column = columns.
                Where(x => options.Match.IsMatch(x.HeaderName, options.Terms)).
                ElementAtOrDefault(options.Index ?? 0);

            return column != null ? BuildXPathForCell(column, columns) : null;
        }

        protected virtual string BuildXPathForCell(ColumnInfo column, List<ColumnInfo> columns)
        {
            string rowSpannedCellXPathCondition = $"count(td) = {columns.Count}";
            int columnIndex = columns.IndexOf(column);

            if (column.HasRowSpan)
            {
                return $"(self::tr | preceding-sibling::tr)[{rowSpannedCellXPathCondition}][last()]/td[{columnIndex + 1}]";
            }
            else
            {
                int countOfPrecedingColumnsWithoutRowSpan = columns.Take(columnIndex).Count(x => !x.HasRowSpan);
                return $"(self::tr[{rowSpannedCellXPathCondition}]/td[{columnIndex + 1}] | self::tr[not({rowSpannedCellXPathCondition})]/td[{countOfPrecedingColumnsWithoutRowSpan + 1}])";
            }
        }

        protected virtual List<ColumnInfo> GetColumnInfoItems(IWebElement rowScope)
        {
            var headers = rowScope.GetAll(By.XPath(HeaderXPath).AtOnce().OfAnyVisibility());

            var cells = GetCellsOfRowWithSpannedCells(rowScope);

            return headers.Select((header, index) =>
            {
                string cellRowSpanValue = cells.ElementAtOrDefault(index)?.GetAttribute("rowspan")?.Trim();

                return new ColumnInfo
                {
                    HeaderName = header.Text,
                    HasRowSpan = !string.IsNullOrEmpty(cellRowSpanValue) && cellRowSpanValue != "1"
                };
            }).ToList();
        }

        private ReadOnlyCollection<IWebElement> GetCellsOfRowWithSpannedCells(IWebElement rowScope)
        {
            ReadOnlyCollection<IWebElement> cells = rowScope.GetAll(
                By.XPath($"../tr[{RowWithSpannedCellsXPathCondition}][1]/td").AtOnce().OfAnyVisibility());

            return cells.Any()
                ? cells
                : rowScope.GetAll(By.XPath("/td").AtOnce().OfAnyVisibility());
        }

        protected class ColumnInfo
        {
            public string HeaderName { get; set; }

            public bool HasRowSpan { get; set; }
        }
    }
}
