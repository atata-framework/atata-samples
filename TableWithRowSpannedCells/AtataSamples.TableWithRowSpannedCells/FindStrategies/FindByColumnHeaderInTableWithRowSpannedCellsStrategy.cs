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
