using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using QAssistant.Enums;

namespace QAssistant
{
    public class WebDriverFactory
    {
        public static IWebDriver Create(BrowserType browserType)
        {
            return browserType switch
            {
                BrowserType.Chrome => new ChromeDriver(),
                BrowserType.Firefox => new FirefoxDriver(),
                BrowserType.Edge =>
                    // Edge 18 or greater is installed via command line.  See docs for more info.
                    new EdgeDriver(),
                BrowserType.IE11 => new InternetExplorerDriver(),
                _ => throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null)
            };
        }

        public static IWebDriver Create(BrowserType browserType, string driverPath)
        {
            return browserType switch
            {
                BrowserType.Chrome => new ChromeDriver(driverPath),
                BrowserType.Firefox => new FirefoxDriver(driverPath),
                BrowserType.Edge =>
                    // Edge 18 or greater is installed via command line.  See docs for more info.
                    new EdgeDriver(driverPath),
                BrowserType.IE11 => new InternetExplorerDriver(driverPath),
                _ => throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null)
            };
        }
    }
}