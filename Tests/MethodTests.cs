using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using QAssistant;

namespace Tests
{
    public class Tests
    {
        private QAWebDriver<ChromeDriver> _driver;
        
        [SetUp]
        public void Setup()
        {
            var opts = new ChromeOptions();
            opts.AddArgument("--start-maximized");
            _driver = new QAWebDriver<ChromeDriver>(opts);
            _driver.Navigate().GoToUrl("https://google.com");
        }

        [Test]
        public void TestClickMethodOnNoSuchElementException()
        {
            Assert.Throws<NoSuchElementException>(() => _driver.FindElement(By.XPath("incorrectxpath")));
        }
        
        [Test]
        public void TestFindElementOnSuccess()
        {
            Assert.IsInstanceOf(typeof(IWebElement),_driver.FindElement(By.XPath("//*[@id='hplogo']")));
        }
        
        [Test]
        public void TestFindElementMethodOnNoSuchElementException()
        {
            Assert.Throws<NoSuchElementException>(() => _driver.FindElement(By.XPath($"incorrectxpath")));
        }
        
        [Test]
        public void TestClearMethodOnSuccess()
        {
            _driver.EnterText(By.Name($"q"), "test text");
            Assert.DoesNotThrow(() => _driver.ClearField(By.Name("q")));
        }
        
        [Test]
        public void TestElementIsVisibleOnSuccess()
        {
            
            Assert.True(_driver.ElementIsVisible(By.XPath($"//*[@id='hplogo']")));
        }

        [Test]
        public void TestElementIsVisibleOnFail()
        {
            Assert.False(_driver.ElementIsVisible(By.XPath($"incorrectxpath")));
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}