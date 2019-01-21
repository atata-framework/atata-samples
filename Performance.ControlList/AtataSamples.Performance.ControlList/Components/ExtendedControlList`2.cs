using System.Collections.Generic;
using System.Linq;
using Atata;
using OpenQA.Selenium;

namespace AtataSamples.Performance.ControlList
{
    public class ExtendedControlList<TItem, TOwner> : ControlList<TItem, TOwner>
        where TItem : Control<TOwner>
        where TOwner : PageObject<TOwner>
    {
        protected const string GetElementsDataScript = @"
var elements = arguments[0];
var textValues = [];

for (var i = 0; i < elements.length; i++) {
    textValues.push(elements[i].{0});
}

return textValues;";

        public DataProvider<IEnumerable<string>, TOwner> SelectContents(string dataProviderName, string elementXPath, TermOptions dataTermOptions = null)
        {
            return SelectContents<string>(dataProviderName, elementXPath, dataTermOptions);
        }

        public DataProvider<IEnumerable<TData>, TOwner> SelectContents<TData>(string dataProviderName, string elementXPath, TermOptions dataTermOptions = null)
        {
            return SelectData<TData>(dataProviderName, elementXPath, "textContent", dataTermOptions);
        }

        public DataProvider<IEnumerable<TData>, TOwner> SelectData<TData>(string dataProviderName, string elementXPath, string elementDataJSPath, TermOptions dataTermOptions = null)
        {
            return Component.GetOrCreateDataProvider(
                $"\"{dataProviderName}\" of {ProviderName}",
                () => SelectDataValues<TData>(elementXPath, elementDataJSPath, dataTermOptions));
        }

        protected virtual IEnumerable<TData> SelectDataValues<TData>(string elementXPath, string elementDataJSPath, TermOptions dataTermOptions)
        {
            string fullElementXPath = BuildXPathToSelectData(elementXPath);

            // TODO: Add filtering by visibility.
            var elements = GetItemElements(By.XPath(fullElementXPath).OfAnyVisibility());

            return GetElementsData(elements, elementDataJSPath).
                Select(x => TermResolver.FromString<TData>(x, dataTermOptions));
        }

        private string BuildXPathToSelectData(string subElementXPath)
        {
            string itemOuterXPath = ItemFindAttribute.OuterXPath ?? ".//";
            string itemXPath = itemOuterXPath + ItemDefinition.ScopeXPath;

            if (string.IsNullOrWhiteSpace(subElementXPath))
                return itemXPath;
            else if (subElementXPath.StartsWith("."))
                return itemXPath + "/" + subElementXPath;
            else if (subElementXPath.StartsWith("/"))
                return itemXPath + subElementXPath;
            else
                return itemXPath + "//" + subElementXPath;
        }

        private IEnumerable<string> GetElementsData(IEnumerable<IWebElement> elements, string elementDataJSPath)
        {
            string formattedScript = GetElementsDataScript.Replace("{0}", elementDataJSPath);

            return ((IEnumerable<object>)AtataContext.Current.Driver.ExecuteScript(formattedScript, elements)).
                Cast<string>().
                Select(x => x.Trim()).
                ToArray();
        }
    }
}
