# QAssistant

QAssistant is created for testers who is working for UI automation using Selenium and WebDriver.

## Installation

Use the package manager to install QAssistant

```bash
Install-Package QAssistant
```
or the .NET CLI.

```bash
dotnet add package QAssistant
```

## Usage example

```csharp
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using QAssistant.Extensions;

namespace Tests
{
    public class ExtensionTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            var opts = new ChromeOptions();
            opts.AddArgument("--start-maximized");
            _driver = new ChromeDriver(opts);
            _driver.Navigate().GoToUrl("https://google.com");
        }

        [Test]
        public void TestReadFromFieldValueOnSuccess()
        {
            Assert.IsEmpty(_driver.ReadFromFieldValue(By.Name("q")));
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.CloseAndDispose();
        }
    }
}
```

# QAssitant 1.1.0
## December 28, 2020

## New Features
* **TakeScreenshot** - Extension methods to take screenshot of the current page and save it with default parameters or pass the image file name, path and format. You can also get screenshot as "AsByteArray" or "AsBase64EncodedString".
* **RandomGenerator** - Helper to generate random symbols, letters, numbers or combination of them. It will be helpful in various cases when it is important to generate random strings or numbers.
* **ElementIdentifier** - Custom attribute for properties. It is created for POM object properties to mark and access them from another classes easly.
```csharp
Example:
        [ElementIdentifier("logoelement")]
        public IWebElement Logo => _driver.WaitUntilElementIsDisplayed(By.Id("hplogo"));
```
* **ElementFinder** - Helper for POM properties to access them with property names or element identifiers from another classes.
```csharp
Example:
        private ElementFinder<GooglePage> _elementFinder;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver(opts);
            _googlePage = new GooglePage(_driver);
            _elementFinder = new ElementFinder<GooglePage>();
            _driver.Navigate().GoToUrl("https://google.com");
        }

        [Test]
        public void TestElementIdentifierOnSuccessWithElementIdentifier()
        {
            Assert.True(typeof(RemoteWebElement) == _elementFinder.FindElement(_googlePage, "logoelement").GetType());
        }

        [Test]
        public void TestElementIdentifierOnSuccessWithPropertyName()
        {
            Assert.True(typeof(RemoteWebElement) == _elementFinder.FindElement(_googlePage, "Logo").GetType());
        }

```
* **WaitUntilElementIsDisplayed** - Extension method finds the first "IWebElement" using the given method and condition and waits for it's visibility.
* **HoverOnElement, ElementExists, GetParent, GetChild, GetPreviousSibling, GetNextSibling** - Extension methods

## Changes
* **WaitUntilFindElement** extension method now finds the first "IWebElement" using the given method and condition and waits for it's existence. 

## Bug Fixes
* Some bug fixes and improvements...



## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://licenses.nuget.org/MIT)
