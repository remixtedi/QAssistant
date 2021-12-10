using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using QAssistant.Extensions;
using QAssistant.WaitHelpers;

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
            opts.AddArgument("--headless");
            _driver = new ChromeDriver(opts);
            _driver.Navigate().GoToUrl("https://www.executeautomation.com/");
        }

        [Test]
        public void TestReadFromFieldValueOnSuccess()
        {
            Assert.IsEmpty(_driver.ReadFromFieldValue(By.XPath("(//input)[1]")));
        }

        [Test]
        public void TestReadFromFieldValueOnFail()
        {
            _driver.WaitUntilFindElement(By.XPath("(//input)[1]")).SendKeys("some text");
            Assert.AreEqual("some text", _driver.ReadFromFieldValue(By.XPath("(//input)[1]")));
        }

        [Test]
        public void TestClearMethodOnSuccess()
        {
            _driver.WaitUntilFindElement(By.XPath("(//input)[1]")).SendKeys("some text");
            Assert.DoesNotThrow(() => _driver.ClearField(By.XPath("(//input)[1]")));
        }

        [Test]
        public void TestElementClassContainsOnSuccess()
        {
            Assert.True(_driver.ElementClassContains(By.XPath("(//input)[1]"), "ct-input"));
        }

        [Test]
        public void TestElementClassContainsOnFail()
        {
            Assert.False(_driver.ElementClassContains(By.XPath("(//input)[1]"), "incorrectclass"));
        }

        [Test]
        public void TestWaitUntilElementIsDisplayedOnSuccess()
        {
            Assert.IsInstanceOf(typeof(IWebElement),
                _driver.WaitUntilElementIsDisplayed(
                    By.XPath("//*[@id=\"root\"]/div/div[1]/div/div/div/div[2]/div/ul/li[4]")));
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
            Assert.IsInstanceOf(typeof(IWebElement), _driver.WaitUntilFindElement(By.ClassName("ct-logo-header")));
        }

        [Test]
        public void TestWaitUntilFindElementOnFail()
        {
            Assert.Throws<WebDriverTimeoutException>(() => _driver.WaitUntilFindElement(By.XPath("incorrectxpath")));
        }

        [Test]
        public void TestFindVisibleElementsOnSuccess()
        {
            Assert.AreEqual(1,
                _driver.WaitUntilFindElements(By.XPath("//*[@id=\"root\"]/div/div[1]/div/div/div/div[2]/div/ul/li[4]"))
                    .Count);
        }

        [Test]
        public void TestFindVisibleElementsOnFail()
        {
            Assert.Throws<WebDriverTimeoutException>(() => _driver.WaitUntilFindElements(By.XPath("incorrectxpath")));
        }

        [Test]
        public void TestIsFieldClearOnSuccess()
        {
            Assert.IsTrue(_driver.IsFieldClear(By.XPath("(//input)[1]")));
        }

        [Test]
        public void TestIsFieldClearOnFail()
        {
            _driver.WaitUntilFindElement(By.XPath("(//input)[1]")).SendKeys("some text");
            Assert.IsFalse(_driver.IsFieldClear(By.XPath("(//input)[1]")));
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
            Assert.Throws<WebDriverTimeoutException>(() => _driver.WaitUntilPageTitleIs("Learning Paths"));
        }

        [Test]
        public void TestScrollIntoViewOnSuccessWithElement()
        {
            Assert.DoesNotThrow(() =>
                _driver.ScrollIntoView(
                    _driver.WaitUntilFindElement(By.XPath("//*[@id=\"ct-path-main\"]/div[1]/div[1]/button"))));
        }

        [Test]
        public void TestScrollIntoViewOnSuccessWithLocator()
        {
            Assert.DoesNotThrow(
                () => _driver.ScrollIntoView(By.XPath("//*[@id=\"ct-path-main\"]/div[1]/div[1]/button")));
        }

        [Test]
        public void TestElementExistsOnSuccess()
        {
            Assert.True(_driver.ElementExists(By.XPath("(//input)[1]")));
        }

        [Test]
        public void TestElementExistsOnFail()
        {
            Assert.False(_driver.ElementExists(By.Name("Invalid")));
        }

        [Test]
        public void TestGetParent()
        {
            Assert.True(typeof(WebElement) ==
                        _driver.WaitUntilFindElement(By.ClassName("ct-landing-page")).GetParent().GetType());
        }

        [Test]
        public void TestGetChild()
        {
            Assert.True(typeof(WebElement) ==
                        _driver.WaitUntilFindElement(By.ClassName("ct-landing-page")).GetChild().GetType());
        }

        [Test]
        public void TestGetNextSibling()
        {
            Assert.True(typeof(WebElement) ==
                        _driver.WaitUntilFindElement(By.ClassName("ct-landing-page")).GetNextSibling().GetType());
        }

        [Test]
        public void TestGetPreviousSibling()
        {
            Assert.True(typeof(WebElement) ==
                        _driver.WaitUntilFindElement(By.ClassName("ct-landing-page")).GetPreviousSibling().GetType());
        }

        [Test]
        public void TestHoverOnElement()
        {
            Assert.DoesNotThrow(() => _driver.HoverOnElement(By.XPath("(//input)[1]")));
        }

        [Test]
        public void TestClick()
        {
            Assert.DoesNotThrow(() => _driver.FindElement(By.XPath("(//input)[1]")).Click());
        }

        [Test]
        public void WaitPositiveTest()
        {
            _driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.XPath("(//input)[1]")));
        }

        [Test]
        public void WaitNegativeTest()
        {
            Assert.Throws<WebDriverTimeoutException>(() => _driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.Name("incorrect"))));
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.CloseAndDispose();
        }
    }
}