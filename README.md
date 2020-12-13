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

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://licenses.nuget.org/MIT)
