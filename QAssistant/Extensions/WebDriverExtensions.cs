using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using QAssistant.WaitHelpers;
using System;

namespace QAssistant.Extensions
{
    public static class WebDriverExtensions
    {
        // Consider storing the DefaultWaitTime in the web.config.
        private const int DefaultWaitTime = 10;
        // Create a default wait time span so we can reuse the most common time span.
        private static readonly TimeSpan DefaultWaitTimeSpan = TimeSpan.FromSeconds(DefaultWaitTime);

        public static IWait<IWebDriver> Wait(this IWebDriver driver) => Wait(driver, DefaultWaitTimeSpan);
        public static IWait<IWebDriver> Wait(this IWebDriver driver, int waitTime) => Wait(driver, TimeSpan.FromSeconds(waitTime));
        public static IWait<IWebDriver> Wait(this IWebDriver driver, TimeSpan waitTimeSpan) => new WebDriverWait(driver, waitTimeSpan);

        public static IWebElement Find(this IWebDriver driver, By locator)
        {
            var el = driver.Wait().Until(condition => ExpectedConditions.ElementIsVisible(locator));
            return driver.FindElement(locator);
        }

        public static IWebElement Find(this IWebDriver driver, By locator, Func<IWebDriver, IWebElement> condition)
        {
            driver.Wait().Until(condition);
            return driver.FindElement(locator);
        }

        public static IWebElement WaitUntilPageTitleIs(this IWebDriver driver, string pageTitle)
        {
            driver.Wait().Until(ExpectedConditions.TitleIs(pageTitle));
            return driver.WaitUntilHtmlTagLoads();
        }
        
        public static void Click(this IWebDriver driver, By by)
        {
            driver.Find(by).Click();
        }

        public static IWebElement WaitUntilPageLoad(this IWebDriver driver, string titleOnNewPage, IWebElement elementOnOldPage)
        {
            driver.Wait().Until(ExpectedConditions.StalenessOf(elementOnOldPage));
            driver.Wait().Until(ExpectedConditions.TitleIs(titleOnNewPage));
            return driver.WaitUntilHtmlTagLoads();
        }

        private static IWebElement WaitUntilHtmlTagLoads(this IWebDriver driver) => driver.Find(By.XPath("html"));

        public static void ScrollIntoView(this IWebDriver driver, IWebElement element)
        {
            // Assumes IWebDriver can be cast as IJavaScriptExecuter.
            ScrollIntoView((IJavaScriptExecutor)driver, element);
        }

        private static void ScrollIntoView(IJavaScriptExecutor driver, IWebElement element)
        {
            driver.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void CloseAndDispose(this IWebDriver driver)
        {

            driver.Close();
            driver.Dispose();
        }
    }
}
