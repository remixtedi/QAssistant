using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using QAssistant.WaitHelpers;

namespace QAssistant.Extensions
{
    public static class WebDriverExtensions
    {
        // Consider storing the DefaultWaitTime in the web.config.
        private const int DefaultWaitTime = 10;

        // Default format for TakeScreenshot method
        private const ScreenshotImageFormat DefaultScreenshotImageFormat = ScreenshotImageFormat.Png;

        // Default image file name for TakeScreenshot method
        private const string DefaultScreenshotImageName = "Screenshot";

        // Default folder name for TakeScreenshot method (it will be added to end of the program execution path)
        private const string DefaultScreenshotsFolderName = "Screenshots";

        // Create a default wait time span so we can reuse the most common time span.
        private static readonly TimeSpan DefaultWaitTimeSpan = TimeSpan.FromSeconds(DefaultWaitTime);

        public static IWait<IWebDriver> Wait(this IWebDriver driver)
        {
            return Wait(driver, DefaultWaitTimeSpan);
        }

        public static IWait<IWebDriver> Wait(this IWebDriver driver, int waitTime)
        {
            return Wait(driver, TimeSpan.FromSeconds(waitTime));
        }

        public static IWait<IWebDriver> Wait(this IWebDriver driver, TimeSpan waitTimeSpan)
        {
            return new WebDriverWait(driver, waitTimeSpan);
        }

        /// <summary>
        ///     Finds the first <see cref="T:OpenQA.Selenium.IWebElement" /> using the given method and condition
        ///     and waits for it's visibility.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="locator">The locating mechanism to use.</param>
        /// <returns>The first matching <see cref="T:OpenQA.Selenium.IWebElement" /> on the current context.</returns>
        public static IWebElement WaitUntilElementIsDisplayed(this IWebDriver driver, By locator)
        {
            return driver.Wait().Until(ExpectedConditions.ElementIsVisible(locator));
        }

        /// <summary>
        ///     Finds all Displayed <see cref="T:OpenQA.Selenium.IWebElement">IWebElements</see> within the current context
        ///     using the given mechanism.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="locator">The locating mechanism to use.</param>
        /// <returns>
        ///     A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of all Displayed
        ///     <see cref="T:OpenQA.Selenium.IWebElement">WebElements</see>
        ///     which matches the current criteria, or an empty list if nothing matches.
        /// </returns>
        public static ReadOnlyCollection<IWebElement> WaitUntilElementsAreDisplayed(this IWebDriver driver, By locator)
        {
            return driver.Wait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
        }

        /// <summary>
        ///     Finds the first <see cref="T:OpenQA.Selenium.IWebElement" /> using the given method and condition
        ///     and waits for it's existence.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="locator">The locating mechanism to use.</param>
        /// <returns>The first matching <see cref="T:OpenQA.Selenium.IWebElement" /> on the current context.</returns>
        public static IWebElement WaitUntilFindElement(this IWebDriver driver, By locator)
        {
            return driver.Wait().Until(ExpectedConditions.ElementExists(locator));
        }

        /// <summary>
        ///     Finds all <see cref="T:OpenQA.Selenium.IWebElement">IWebElements</see> within the current context
        ///     using the given mechanism.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="locator">The locating mechanism to use.</param>
        /// <returns>
        ///     A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of all
        ///     <see cref="T:OpenQA.Selenium.IWebElement">WebElements</see>
        ///     which matches the current criteria, or an empty list if nothing matches.
        /// </returns>
        public static ReadOnlyCollection<IWebElement> WaitUntilFindElements(this IWebDriver driver, By locator)
        {
            return driver.Wait().Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }

        /// <summary>
        ///     Clears the content of this element (INPUT or TEXTAREA tags).
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="selector">The locating mechanism to use.</param>
        /// <exception cref="NoSuchElementException"></exception>
        /// <exception cref="Exception"></exception>
        public static void ClearField(this IWebDriver driver, By selector)
        {
            try
            {
                var element = driver.WaitUntilFindElement(selector);

                if (element == null)
                    throw new NoSuchElementException($"Element ({selector}) wasn't found or it's not visible.");

                element.Clear();
                if (!string.IsNullOrEmpty(driver.ReadFromFieldValue(selector)))
                {
                    element.SendKeys(Keys.Control + "a");
                    element.SendKeys(Keys.Delete);
                }

                if (!string.IsNullOrEmpty(driver.ReadFromFieldValue(selector)))
                    throw new Exception("Text wasn't cleared.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        ///     Gets the state of field value is it empty or not.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="selector">The locating mechanism to use.</param>
        /// <returns>The boolean type result whether the element value is empty or not.</returns>
        public static bool IsFieldClear(this IWebDriver driver, By selector)
        {
            return string.IsNullOrEmpty(driver.FindElement(selector).ReadFromFieldValue());
        }

        /// <summary>
        ///     Gets value from this element (INPUT or TEXTAREA tags).
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="selector">The locating mechanism to use.</param>
        /// <returns>
        ///     The JavaScript property's current value. Returns a <see langword="null" /> if the
        ///     value is not set or the property does not exist.
        /// </returns>
        public static string ReadFromFieldValue(this IWebDriver driver, By selector)
        {
            return driver.FindElement(selector).ReadFromFieldValue();
        }

        /// <summary>
        ///     Check if element class attribute contains this class name.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="selector">The locating mechanism to use.</param>
        /// <param name="className">string type value</param>
        /// <returns>The boolean type result whether the element contains this class or not</returns>
        public static bool ElementClassContains(this IWebDriver driver, By selector, string className)
        {
            try
            {
                return driver.FindElement(selector).GetAttribute("class").ToLower().Contains(className.ToLower());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        ///     Clicks this element.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Click this element. If the click causes a new page to load, the
        ///         <see cref="M:OpenQA.Selenium.IWebElement.Click" />
        ///         method will attempt to block until the page has loaded. After calling the
        ///         <see cref="M:OpenQA.Selenium.IWebElement.Click" /> method, you should discard all references to this
        ///         element unless you know that the element and the page will still be present.
        ///         Otherwise, any further operations performed on this element will have an undefined.
        ///         behavior.
        ///     </para>
        ///     <para>
        ///         If this element is not clickable, then this operation is ignored. This allows you to
        ///         simulate a users to accidentally missing the target when clicking.
        ///     </para>
        /// </remarks>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="selector">The locating mechanism to use.</param>
        /// <exception cref="T:OpenQA.Selenium.ElementNotVisibleException">Thrown when the target element is not visible.</exception>
        /// <exception cref="T:OpenQA.Selenium.StaleElementReferenceException">
        ///     Thrown when the target element is no longer valid in
        ///     the document DOM.
        /// </exception>
        public static void Click(this IWebDriver driver, By selector)
        {
            driver.WaitUntilFindElement(selector).Click();
        }

        /// <summary>
        ///     Waits until page title matches the passed value and then execute WaitUntilHtmlTagLoads,
        ///     where it finds the first <see cref="T:OpenQA.Selenium.IWebElement" /> using the given method.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="titleOnNewPage">Title of the page.</param>
        /// <exception cref="T:OpenQA.Selenium.WebDriverTimeoutException">If element is not visible.</exception>
        /// <returns>The first matching <see cref="T:OpenQA.Selenium.IWebElement" /> on the current context.</returns>
        public static IWebElement WaitUntilPageTitleIs(this IWebDriver driver, string titleOnNewPage)
        {
            driver.Wait().Until(ExpectedConditions.TitleIs(titleOnNewPage));
            return driver.WaitUntilHtmlTagLoads();
        }

        /// <summary>
        ///     Waits until a condition is true or times out and finds the first <see cref="T:OpenQA.Selenium.IWebElement" /> using
        ///     the given method.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="selector">The locating mechanism to use.</param>
        /// <param name="condition">A delegate taking a TSource as its parameter, and returning a TResult.</param>
        /// <returns>The first matching <see cref="T:OpenQA.Selenium.IWebElement" /> on the current context.</returns>
        public static IWebElement WaitUntilPageLoad(this IWebDriver driver, By selector,
            Func<IWebDriver, IWebElement> condition)
        {
            driver.Wait().Until(condition);
            return driver.FindElement(selector);
        }

        /// <summary>
        ///     Waits until an element is no longer attached to the DOM, then waits until page title matches the passed parameter
        ///     and then execute WaitUntilHtmlTagLoads,
        ///     where it finds the first <see cref="T:OpenQA.Selenium.IWebElement" /> using the given method.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="titleOnNewPage">Title of the page</param>
        /// <param name="elementOnOldPage">The <see cref="IWebElement" /></param>
        /// <returns>The <see cref="IWebElement" /> of html tag.</returns>
        public static IWebElement WaitUntilPageLoad(this IWebDriver driver, string titleOnNewPage,
            IWebElement elementOnOldPage)
        {
            driver.Wait().Until(ExpectedConditions.StalenessOf(elementOnOldPage));
            driver.Wait().Until(ExpectedConditions.TitleIs(titleOnNewPage));
            return driver.WaitUntilHtmlTagLoads();
        }

        /// <summary>
        ///     Waits until the html tag is located or times out.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <returns>The <see cref="IWebElement" /> of html tag.</returns>
        private static IWebElement WaitUntilHtmlTagLoads(this IWebDriver driver)
        {
            return driver.WaitUntilFindElement(By.XPath("html"));
        }

        /// <summary>
        ///     Wait for <see cref="IWebElement" /> when it will be visible and scroll into view.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="selector">The locating mechanism to use.</param>
        public static void ScrollIntoView(this IWebDriver driver, By selector)
        {
            driver.ScrollIntoView(driver.WaitUntilFindElement(selector));
        }

        /// <summary>
        ///     Wait for <see cref="IWebElement" /> when it will be visible and scroll into view.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="element">The locating mechanism to use.</param>
        public static void ScrollIntoView(this IWebDriver driver, IWebElement element)
        {
            // Assumes IWebDriver can be cast as IJavaScriptExecuter.
            ScrollIntoView((IJavaScriptExecutor) driver, element);
        }

        /// <summary>
        ///     Wait for <see cref="IWebElement" /> when it will be visible and scroll into view.
        /// </summary>
        /// <param name="driver">The <see cref="IJavaScriptExecutor" />.</param>
        /// <param name="element">The locating mechanism to use.</param>
        private static void ScrollIntoView(IJavaScriptExecutor driver, IWebElement element)
        {
            driver.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        /// <summary>
        ///     Close the current window, quitting the browser if it is the last window currently open
        ///     and dispose an instance of <see cref="IWebDriver" />.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        public static void CloseAndDispose(this IWebDriver driver)
        {
            driver.Close();
            driver.Dispose();
        }

        /// <summary>
        ///     Gets a <see cref="T:OpenQA.Selenium.Screenshot" /> object representing the image of the page on the screen.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <returns>A <see cref="T:OpenQA.Selenium.Screenshot" /> object containing the image.</returns>
        public static Screenshot TakeScreenshotAsScreenshot(this IWebDriver driver)
        {
            return ((ITakesScreenshot) driver).GetScreenshot();
        }

        /// <summary>
        ///     Takes a screenshot of the page and saves it in <see cref="DefaultScreenshotsFolderName" /> folder with
        ///     <see cref="DefaultScreenshotImageName" /> and <see cref="DefaultScreenshotImageFormat" />.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <returns>The <see cref="string" /> type value of image path.</returns>
        public static string TakeScreenshot(this IWebDriver driver)
        {
            return driver.TakeScreenshot(DefaultScreenshotImageName,
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Directory.GetCurrentDirectory(),
                    DefaultScreenshotsFolderName),
                DefaultScreenshotImageFormat);
        }

        /// <summary>
        ///     Takes a screenshot of the page and saves it in <see cref="DefaultScreenshotsFolderName" /> folder with passed file
        ///     name and <see cref="DefaultScreenshotImageFormat" />.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="fileName">The image file name./</param>
        /// <returns>The <see cref="string" /> type value of image path.</returns>
        public static string TakeScreenshot(this IWebDriver driver, string fileName)
        {
            return driver.TakeScreenshot(fileName,
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Directory.GetCurrentDirectory(),
                    DefaultScreenshotsFolderName),
                DefaultScreenshotImageFormat);
        }

        /// <summary>
        ///     Takes a screenshot of the page and saves it in passed file path with passed file name and
        ///     <see cref="DefaultScreenshotImageFormat" />.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="fileName">The image file name.</param>
        /// <param name="filePath">The full path to save the screenshot to.</param>
        /// <returns>The <see cref="string" /> type value of image path.</returns>
        public static string TakeScreenshot(this IWebDriver driver, string fileName, string filePath)
        {
            return driver.TakeScreenshot(fileName, filePath, DefaultScreenshotImageFormat);
        }

        /// <summary>
        ///     Takes a screenshot of the page and saves it in passed file path with passed file name and file format.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="fileName">The image file name.</param>
        /// <param name="filePath">The The full path and file name to save the screenshot to..</param>
        /// <param name="imageFormat">
        ///     A <see cref="T:OpenQA.Selenium.ScreenshotImageFormat" /> value indicating the format
        ///     to save the image to.
        /// </param>
        /// <returns>The <see cref="string" /> type value of image path.</returns>
        public static string TakeScreenshot(this IWebDriver driver, string fileName, string filePath,
            ScreenshotImageFormat imageFormat)
        {
            var screen = driver.TakeScreenshotAsScreenshot().AsByteArray;
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
            var filePathAndName = Path.Combine(filePath,
                $"{fileName}-{DateTime.UtcNow.Ticks}.{imageFormat}");
            using var save = File.Create(filePathAndName);
            save.Write(screen);
            return filePathAndName;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="T:OpenQA.Selenium.IWebElement" /> exists on page.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="locator">The locating mechanism to use.</param>
        /// <returns><c>true</c> if the specified element is found; otherwise, <c>false</c>.</returns>
        public static bool ElementExists(this IWebDriver driver, By locator)
        {
            try
            {
                driver.Wait().Until(ExpectedConditions.ElementExists(locator));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Hovers over specified element.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" />.</param>
        /// <param name="locator">The locating mechanism to use.</param>
        public static void HoverOnElement(this IWebDriver driver, By locator)
        {
            var action = new Actions(driver);
            action.MoveToElement(driver.WaitUntilFindElement(locator)).Perform();
        }
    }
}