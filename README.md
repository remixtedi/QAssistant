# QAssistant

QAssistant is created for automation engineers and testers who are working for UI automation using Selenium and WebDriver.

## Installation

Use the package manager to install QAssistant

```bash
Install-Package QAssistant
```
or the .NET CLI.

```bash
dotnet add package QAssistant
```

# Usage examples

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
        public void TestGetChild()
        {
            Assert.True(typeof(WebElement) == _driver.WaitUntilFindElement(By.TagName("center")).GetChild().GetType());
        }
        
        [Test]
        public void TestHoverOnElement()
        {
            Assert.DoesNotThrow(() => _driver.HoverOnElement(By.Name("q")));
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.CloseAndDispose();
        }
    }
}
```

## TakeScreenshot
Extension methods to take a screenshot of the current page and save it with default parameters or pass the image file name, path, and format. You can also get a screenshot as “AsByteArray” or “AsBase64EncodedString”.
```csharp
Example:
        [Test]
        public void TakeScreenshotAsScreenshotTestType()
        {
            Assert.True(typeof(Screenshot) = _driver.TakeScreenshotAsScreenshot().GetType());
        }
        
        [Test]
        public void TakeScreenshotTestWhenFileNameIsPassed()
        {
            var file = _driver.TakeScreenshot("screenfromtest");
            Assert.That(File.Exists(file));
            File.Delete(file);
        }
```

## RandomGenerator
Helper to generate random symbols, letters, numbers, or combinations of them. It will be helpful in various cases when it is important to generate random strings or numbers.
```csharp
Example:
        [Test]
        public void GeneratesNumbersAndAlphabeticalCharacters4()
        {
            Assert.True(_generator.RandomDigitsAndLetters(10).Length == 10);
        }
        
        [Test]
        public void RandomNumberReturnsMin10Max20()
        {
            var value = _generator.RandomNumber(10, 20);
            Assert.IsTrue(value >= 10 && value <= 20);
        }
```

## ElementIdentifier
Custom attribute for properties. It is created for POM object properties to mark and access them from other classes easily.
```csharp
Example:
        [ElementIdentifier("logoelement")]
        public IWebElement Logo => _driver.WaitUntilElementIsDisplayed(By.Id("hplogo"));
```
## ElementFinder
Helper for POM properties to access them with property names or element identifiers from other classes.
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
            Assert.True(typeof(WebElement) == _elementFinder.FindElement(_googlePage, "logoelement").GetType());
        }

        [Test]
        public void TestElementIdentifierOnSuccessWithPropertyName()
        {
            Assert.True(typeof(WebElement) == _elementFinder.FindElement(_googlePage, "Logo").GetType());
        }
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://licenses.nuget.org/MIT)
