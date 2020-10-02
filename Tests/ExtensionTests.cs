using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using QAssistant;
using QAssistant.Enums;
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
            _driver = WebDriverFactory.Create(BrowserType.Chrome);
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
        public void TestFindVisibleElementOnSuccess()
        {
            Assert.IsInstanceOf(typeof(IWebElement), _driver.WaitUntilFindElement(By.Id("hplogo")));
        }
        
        [Test]
        public void TestFindVisibleElementOnFail()
        {
            Assert.Throws<WebDriverTimeoutException>(() => _driver.WaitUntilFindElement(By.XPath("incorrectxpath")));
        }
        
        [Test]
        public void TestFindVisibleElementsOnSuccess()
        {
            Assert.AreEqual(1, _driver.WaitUntilFindElements(By.Id("hplogo")).Count);
        }
        
        [Test]
        public void TestFindVisibleElementsOnFail()
        {
            Assert.AreEqual(0, _driver.WaitUntilFindElements(By.XPath("incorrectxpath")).Count);
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
            _driver.Click(By.XPath("//*[@id='gbw']/div/div/div[1]/div[2]/a"));
            Assert.IsInstanceOf<IWebElement>(_driver.WaitUntilPageTitleIs("გუგლის სურათები"));
        }
                
        [Test]
        public void TestWaitUntilPageTitleIsOnFail()
        {
            _driver.Click(By.XPath("//*[@id='gbw']/div/div/div[1]/div[1]/a"));
            Assert.Throws<WebDriverTimeoutException>(() => _driver.WaitUntilPageTitleIs("გუგლის სურათები"));
        }
        
        [Test]
        public void TestScrollIntoViewOnSuccessWithElement()
        {
            _driver.WaitUntilFindElement(By.Name("q")).SendKeys("some text");
            _driver.WaitUntilFindElement(By.Name("q")).SendKeys(Keys.Enter);
            Assert.DoesNotThrow(() => _driver.ScrollIntoView(_driver.WaitUntilFindElement(By.XPath("//*[@id='rso']/div[4]"))));
        }
                
        [Test]
        public void TestScrollIntoViewOnSuccessWithLocator()
        {
            _driver.WaitUntilFindElement(By.Name("q")).SendKeys("some text");
            _driver.WaitUntilFindElement(By.Name("q")).SendKeys(Keys.Enter);
            Assert.DoesNotThrow(() => _driver.ScrollIntoView(By.XPath("//*[@id='rso']/div[4]")));
        }
        
        [Test]
        public void DumbTest()
        {
            // _driver.SwitchTo().Alert();
            // _driver.FindElement(By.Id("hplogo"));
        }
        
        [TearDown]
        public void CloseBrowser()
        {
            _driver.CloseAndDispose();
        }
    }
}