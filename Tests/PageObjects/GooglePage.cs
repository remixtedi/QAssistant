using System.Collections.ObjectModel;
using OpenQA.Selenium;
using QAssistant.Extensions;
using QAssistant.Helpers;

namespace Tests.PageObjects
{
    public class GooglePage
    {
        private readonly IWebDriver _driver;

        public GooglePage(IWebDriver driver)
        {
            _driver = driver;
        }

        [ElementIdentifier("logoelement")]
        public IWebElement Logo => _driver.WaitUntilElementIsDisplayed(By.XPath("/html/body/div[1]/div[2]/div/img"));

        [ElementIdentifier("links")]
        public ReadOnlyCollection<IWebElement> Links => _driver.FindElements(By.XPath("//a"));
    }
}