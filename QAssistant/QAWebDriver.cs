using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using QAssistant.Extensions;
using QAssistant.WaitHelpers;

namespace QAssistant
{
    public class QAWebDriver<T> where T : IWebDriver, new()
    {
        IWebDriver driver;
        public string CurrentTest { get; set; }

        public QAWebDriver()
        {
            driver = new T();
        }
        
        public QAWebDriver(DriverOptions opts)
        {
            driver = this.GetType().GenericTypeArguments[0].Name switch
            {
                "ChromeDriver" => new ChromeDriver((ChromeOptions) opts),
                "FirefoxDriver" => new FirefoxDriver((FirefoxOptions) opts),
                "EdgeDriver" => new EdgeDriver(),
                "InternetExplorerDriver" => new InternetExplorerDriver(),
                _ => throw new ArgumentOutOfRangeException(nameof(T), typeof(T), null)
            };
        }


        #region Driver Actions

        public void Dispose()
        {
            driver.Dispose();
        }

        public void Close()
        {
            driver.Close();
        }

        public void Quit()
        {
            driver.Quit();
        }

        public IOptions Manage()
        {
            return driver.Manage();
        }

        public INavigation Navigate()
        {
            return driver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            return SwitchTo();
        }

        public string Url
        {
            get
            {
                return driver.Url;
            }
            set
            {
                driver.Url = value;
            }
        }

        public string Title
        {
            get
            {
                return driver.Title;
            }
        }

        public string PageSource
        {
            get
            {
                return driver.PageSource;
            }
        }

        public string CurrentWindowHandle
        {
            get
            {
                return driver.CurrentWindowHandle;
            }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get
            {
                return WindowHandles;
            }
        }

        #endregion

        #region Actions On Element
        /// <summary>
        /// Finds the first <see cref="IWebElement"/> using the given method.
        /// </summary>
        /// <param name="by">The locator used to find the element.</param>
        /// <param name="mustBeVisible">Optional boolean type value if element must be visible or not.</param>
        /// <returns>The first matching <see cref="IWebElement"/> on the current context</returns>

        public IWebElement FindElement(By by, bool mustBeVisible = false)
        {
            try
            {
                if (mustBeVisible)
                {
                    return WaitAndReturnElementWhenVisible(by);
                }

                return driver.FindElement(by);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Finds all IWebElements within the current context using the given mechanism.
        /// </summary>
        /// <param name="by">The locator used to find the element.</param>
        /// <returns>A <see cref="ReadOnlyCollection"/> of all WebElements matching the current criteria, or an empty list if nothing matches.</returns>
        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            try
            {
                return driver.FindElements(by);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Clicks this element.
        /// </summary>
        /// <param name="by">The locator used to find the element.</param>
        /// <remarks>Click this element. If the click causes a new page to load, the OpenQA.Selenium.IWebElement.Click
        ///     method will attempt to block until the page has loaded. After calling the OpenQA.Selenium.IWebElement.Click
        ///     method, you should discard all references to this element unless you know that
        ///     the element and the page will still be present. Otherwise, any further operations
        ///     performed on this element will have an undefined. behavior.
        ///     If this element is not clickable, then this operation is ignored. This allows
        ///     you to simulate a users to accidentally missing the target when clicking.</remarks>
        public void Click(By by)
        {
            try
            {
                FindElement(by, true).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Enters a text in this element.
        /// </summary>
        /// <param name="by">The locator used to find the element.</param>
        /// <param name="text">string type value</param>
        public void EnterText(By by, string text)
        {
            try
            {
                FindElement(by).SendKeys(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Wait and return IWebElement when it will be visible.
        /// </summary>
        /// <param name="by">The locator used to find the element.</param>
        /// <returns>The <see cref="IWebElement"/> once it is located and visible.</returns>
        public IWebElement WaitAndReturnElementWhenVisible(By by)
        {
            try
            {
                return driver.Wait().Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Clears the content of this element (INPUT or TEXTAREA tags).
        /// </summary>
        /// <param name="by">The locator used to find the element.</param>
        public void ClearField(By by)
        {
            var element = WaitAndReturnElementWhenVisible(by);
            try
            {
                if (element == null)
                {
                    throw new NoSuchElementException($"Element ({by}) wasn't found or it's not visible.");
                }

                element.Clear();
                if (!string.IsNullOrEmpty(ReadFromFieldValue(by)))
                {
                    EnterText(by, Keys.Control + "a");
                    EnterText(by, Keys.Delete);
                }
                if (!string.IsNullOrEmpty(ReadFromFieldValue(by)))
                {
                    throw new Exception("Text wasn't cleared.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets value from this element (INPUT or TEXTAREA tags).
        /// </summary>
        /// <param name="by">The locator used to find the element.</param>
        /// <returns>string type value from this element</returns>
        public string ReadFromFieldValue(By by)
        {
            return FindElement(by).GetProperty("value");
        }

        public bool IsFieldClear(By by)
        {
            return string.IsNullOrEmpty(FindElement(by).GetProperty("value"));
        }

        /// <summary>
        /// Waits and returns a boolean type value if element is visible or not.
        /// </summary>
        /// <param name="by">The locator used to find the element.</param>
        /// <returns>boolean type value.</returns>
        public bool ElementIsVisible(By by)
        {
            try
            {
                driver.Wait().Until(ExpectedConditions.ElementIsVisible(by));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool ElementIsEnabled(By by)
        {
            try
            {
                return FindElement(by, true).Enabled;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool ElementClassContains(By by, string className)
        {
            try
            {
                return FindElement(by).GetAttribute("class").ToLower().Contains(className.ToLower());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        #endregion
    }
}