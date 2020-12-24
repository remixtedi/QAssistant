using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using QAssistant.Extensions;
using QAssistant.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    class ScreenshotTests
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

        public void TaceScreenshotTest()
        {
            for (int i = 0; i < 12; i++)
            {
                //_driver.TakeScreenshot(@"C:\Users\iliaa\Desktop\asad", "png");
                //_driver.TakeScreenshot("Screen", @"C:\Users\iliaa\Desktop\asad","png");
                //_driver.TakeScreenshot();

            }
            _driver.GetScreenshotAsBytes();

        }


        [TearDown]
        public void CloseBrowser()
        {
            _driver.CloseAndDispose();
        }

    }
}
