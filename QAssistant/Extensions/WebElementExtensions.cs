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

        /// <summary>
        ///     Gets the specified elements parent element.
        /// </summary>
        /// <param name="element">The <see cref="IWebElement" />.</param>
        /// <returns>The parent element</returns>
        public static IWebElement GetParent(this IWebElement element)
        {
            return element.FindElement(By.XPath("./parent::*"));
        }

        /// <summary>
        ///     Gets the specified elements child element.
        /// </summary>
        /// <param name="element">The <see cref="IWebElement" />.</param>
        /// <returns>The child element</returns>
        public static IWebElement GetChild(this IWebElement element)
        {
            return element.FindElement(By.XPath("./child::*"));
        }

        /// <summary>
        ///     Gets the preceding elements sibling.
        /// </summary>
        /// <param name="element">The <see cref="IWebElement" />.</param>
        /// <returns>The preceding elements sibling element.</returns>
        public static IWebElement GetPreviousSibling(this IWebElement element)
        {
            return element.FindElement(By.XPath("./preceding-sibling::*"));
        }

        /// <summary>
        ///     Gets the following elements sibling.
        /// </summary>
        /// <param name="element">The <see cref="IWebElement" />.</param>
        /// <returns>The following elements sibling element.</returns>
        public static IWebElement GetNextSibling(this IWebElement element)
        {
            return element.FindElement(By.XPath("./following-sibling::*"));
        }
    }
}