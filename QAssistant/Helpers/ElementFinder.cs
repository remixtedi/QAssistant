using System;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace QAssistant.Helpers
{
    public class ElementFinder<T>
    {
        /// <summary>
        ///     Finds the first <see cref="T:OpenQA.Selenium.IWebElement" /> from page object using the
        ///     <see cref="T:QAssistant.Helpers.ElementIdentifier" /> attribute or property name.
        /// </summary>
        /// <param name="page">The page object to use.</param>
        /// <param name="elementIdentifier">The element identifier to use.</param>
        /// <returns>The first matching <see cref="T:OpenQA.Selenium.IWebElement" /> on the current page object.</returns>
        public IWebElement FindElement(T page, string elementIdentifier)
        {
            var value = GetValue(typeof(RemoteWebElement), page, elementIdentifier);
            return (IWebElement) value;
        }

        /// <summary>
        ///     Finds the first collection of <see cref="T:OpenQA.Selenium.IWebElement" /> type objects from page object using the
        ///     <see cref="T:QAssistant.Helpers.ElementIdentifier" /> attribute or property name.
        /// </summary>
        /// <param name="page">The page object to use.</param>
        /// <param name="elementIdentifier">The element identifier to use.</param>
        /// <returns>The first matching <see cref="T:OpenQA.Selenium.IWebElement">IWebElements</see> on the current page object.</returns>
        public ReadOnlyCollection<IWebElement> FindElements(T page, string elementIdentifier)
        {
            var value = GetValue(typeof(ReadOnlyCollection<IWebElement>), page, elementIdentifier);
            return (ReadOnlyCollection<IWebElement>) value;
        }

        private static object GetValue(Type type, T page, string elementIdentifier)
        {
            elementIdentifier = elementIdentifier.ToLower();
            var props = page.GetType().GetProperties();

            if (props.Any(propInfo =>
                propInfo.Name.ToLower() == elementIdentifier && propInfo.GetValue(page)?.GetType() == type))
                return props.First(propInfo =>
                        propInfo.Name.ToLower() == elementIdentifier &&
                        propInfo.GetValue(page, null)?.GetType() == type)
                    .GetValue(page, null);
            if (props.Any(propInfo => propInfo.CustomAttributes.Any(attrData =>
                                          attrData.AttributeType == typeof(ElementIdentifier)
                                          && attrData.ConstructorArguments.Any(arg =>
                                              ((string) arg.Value)?.ToLower() == elementIdentifier))
                                      && propInfo.GetValue(page, null)?.GetType() == type))
                return props.First(propInfo => propInfo.CustomAttributes
                        .Any(attrData => attrData.AttributeType == typeof(ElementIdentifier)
                                         && attrData.ConstructorArguments.Any(arg =>
                                             ((string) arg.Value)?.ToLower() == elementIdentifier)
                                         && propInfo.GetValue(page, null)?.GetType() == type))
                    .GetValue(page, null);

            return null;
        }
    }
}