using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using QAssistant.Extensions;
using QAssistant.Helpers;

namespace Tests
{
    public class ElementFinderTests
    {
        private IWebDriver _driver;
        private GooglePage _googlePage;
        private ElementFinder<GooglePage> _elementFinder;

        [SetUp]
        public void Setup()
        {
            var opts = new ChromeOptions();
            opts.AddArgument("--start-maximized");
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
        public void TestElementIdentifierOnFailWithElementIdentifier()
        {
            Assert.IsNull(_elementFinder.FindElement(_googlePage, "InvalidIdentifier"));
        }

        [Test]
        public void TestElementIdentifierOnSuccessWithPropertyName()
        {
            Assert.True(typeof(RemoteWebElement) == _elementFinder.FindElement(_googlePage, "Logo").GetType());
        }

        [Test]
        public void TestElementIdentifierOnFailWithPropertyName()
        {
            Assert.IsNull(_elementFinder.FindElement(_googlePage, "InvalidName"));
        }

        [Test]
        public void TestElementFinderFindElementsOnSuccess()
        {
            Assert.True(typeof(ReadOnlyCollection<IWebElement>) ==
                        _elementFinder.FindElements(_googlePage, "links").GetType());
        }

        [Test]
        public void TestElementFinderFindElementsOnFail()
        {
            Assert.IsNull(_elementFinder.FindElements(_googlePage, "InvalidIdentifier"));
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.CloseAndDispose();
        }
    }
}
