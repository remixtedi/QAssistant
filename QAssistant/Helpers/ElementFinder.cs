using System;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace QAssistant.Helpers
{
    public class ElementFinder<T>
    {
        public IWebElement Find(T page, string elementIdentifier)
        {
            var value = GetValue(typeof(RemoteWebElement), page, elementIdentifier);
            return (IWebElement) value;
        }

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