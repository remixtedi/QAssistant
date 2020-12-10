using OpenQA.Selenium;

namespace QAssistant.Extensions
{
    public static class WebElementExtensions
    {
        /// <summary>
        ///     Gets value from this element (INPUT or TEXTAREA tags).
        /// </summary>
        /// <param name="element">The <see cref="IWebElement" />.</param>
        /// <returns>
        ///     The JavaScript property's current value. Returns a <see langword="null" /> if the
        ///     value is not set or the property does not exist.
        /// </returns>
        public static string ReadFromFieldValue(this IWebElement element)
        {
            return element.GetProperty("value");
        }
    }
}