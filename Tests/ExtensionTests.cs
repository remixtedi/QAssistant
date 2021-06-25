using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
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

        [Test]
        public void TestReadFromFieldValueOnFail()
        {
            _driver.WaitUntilFindElement(By.Name("q")).SendKeys("some text");
            Assert.AreEqual("some text", _driver.ReadFromFieldValue(By.Name("q")));
        }

        [Test]
        public void TestClearMethodOnSuccess()
        {
            _driver.WaitUntilFindElement(By.Name("q")).SendKeys("some text");
            Assert.DoesNotThrow(() => _driver.ClearField(By.Name("q")));
        }

        [Test]
        public void TestElementClassContainsOnSuccess()
        {
            Assert.True(_driver.ElementClassContains(By.Name("q"), "gLFyf"));
        }

        [Test]
        public void TestElementClassContainsOnFail()
        {
            Assert.False(_driver.ElementClassContains(By.Name("q"), "incorrectclass"));
        }

        [Test]
        public void TestWaitUntilElementIsDisplayedOnSuccess()
        {
            Assert.IsInstanceOf(typeof(IWebElement),
                _driver.WaitUntilElementIsDisplayed(By.XPath("/html/body/div[1]/div[2]/div/img")));
        }

        [Test]
        public void TestWaitUntilElementIsDisplayedOnFail()
        {
            Assert.Throws<WebDriverTimeoutException>(() =>
                _driver.WaitUntilElementIsDisplayed(By.XPath("incorrectxpath")));
        }

        [Test]
        public void TestWaitUntilFindElementOnSuccess()
        {
            Assert.IsInstanceOf(typeof(IWebElement), _driver.WaitUntilFindElement(By.Name("btnK")));
        }

        [Test]
        public void TestWaitUntilFindElementOnFail()
        {
            Assert.Throws<WebDriverTimeoutException>(() => _driver.WaitUntilFindElement(By.XPath("incorrectxpath")));
        }

        [Test]
        public void TestFindVisibleElementsOnSuccess()
        {
            Assert.AreEqual(1, _driver.WaitUntilFindElements(By.XPath("/html/body/div[1]/div[2]/div/img")).Count);
        }

        [Test]
        public void TestFindVisibleElementsOnFail()
        {
            Assert.Throws<WebDriverTimeoutException>(() => _driver.WaitUntilFindElements(By.XPath("incorrectxpath")));
        }

        [Test]
        public void TestIsFieldClearOnSuccess()
        {
            Assert.IsTrue(_driver.IsFieldClear(By.Name("q")));
        }

        [Test]
        public void TestIsFieldClearOnFail()
        {
            _driver.WaitUntilFindElement(By.Name("q")).SendKeys("some text");
            Assert.IsFalse(_driver.IsFieldClear(By.Name("q")));
        }

        [Test]
        public void TestWaitUntilPageTitleIsOnSuccess()
        {
            _driver.Navigate().GoToUrl("https://www.google.ge/imghp");
            Assert.DoesNotThrow(() => _driver.WaitUntilPageTitleIs("გუგლის სურათები"));
        }

        [Test]
        public void TestWaitUntilPageTitleIsOnFail()
        {
            Assert.Throws<WebDriverTimeoutException>(() => _driver.WaitUntilPageTitleIs("გუგლის სურათები"));
        }

        [Test]
        public void TestScrollIntoViewOnSuccessWithElement()
        {
            _driver.WaitUntilFindElement(By.Name("q")).SendKeys("some text");
            _driver.WaitUntilFindElement(By.Name("q")).SendKeys(Keys.Enter);
            Assert.DoesNotThrow(() =>
                _driver.ScrollIntoView(_driver.WaitUntilFindElement(By.XPath("//*[@id='rso']/div[4]"))));
        }

        [Test]
        public void TestScrollIntoViewOnSuccessWithLocator()
        {
            _driver.WaitUntilFindElement(By.Name("q")).SendKeys("some text");
            _driver.WaitUntilFindElement(By.Name("q")).SendKeys(Keys.Enter);
            Assert.DoesNotThrow(() => _driver.ScrollIntoView(By.XPath("//*[@id='rso']/div[4]")));
        }

        [Test]
        public void TestElementExistsOnSuccess()
        {
            Assert.True(_driver.ElementExists(By.Name("q")));
        }

        [Test]
        public void TestElementExistsOnFail()
        {
            Assert.False(_driver.ElementExists(By.Name("Invalid")));
        }

        [Test]
        public void TestGetParent()
        {
            Assert.True(typeof(RemoteWebElement) ==
                        _driver.WaitUntilFindElement(By.TagName("center")).GetParent().GetType());
        }

        [Test]
        public void TestGetChild()
        {
            Assert.True(typeof(RemoteWebElement) ==
                        _driver.WaitUntilFindElement(By.TagName("center")).GetChild().GetType());
        }

        [Test]
        public void TestGetNextSibling()
        {
            Assert.True(typeof(RemoteWebElement) ==
                        _driver.WaitUntilFindElement(By.Name("btnK")).GetNextSibling().GetType());
        }

        [Test]
        public void TestGetPreviousSibling()
        {
            Assert.True(typeof(RemoteWebElement) ==
                        _driver.WaitUntilFindElement(By.Name("q")).GetPreviousSibling().GetType());
        }

        [Test]
        public void TestHoverOnElement()
        {
            Assert.DoesNotThrow(() => _driver.HoverOnElement(By.Name("q")));
        }

        [Test]
        public void TestClick()
        {
            Assert.DoesNotThrow(() => _driver.FindElement(By.Name("q")).Click());
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.CloseAndDispose();
        }
    }
}