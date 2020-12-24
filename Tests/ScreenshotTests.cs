using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using QAssistant.Extensions;

namespace Tests
{
    public class ScreenshotTests
    {
        private IWebDriver _driver;

        private readonly string _defaultScreenshotsPath =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Directory.GetCurrentDirectory(),
                nameof(ScreenshotTests));

        [SetUp]
        public void Setup()
        {
            var opts = new ChromeOptions();
            opts.AddArgument("--start-maximized");
            _driver = new ChromeDriver(opts);
            _driver.Navigate().GoToUrl("https://google.com");
            if (!Directory.Exists(_defaultScreenshotsPath)) Directory.CreateDirectory(_defaultScreenshotsPath);
        }


        [Test]
        public void TakeScreenshotTestForLoop()
        {
            for (var i = 0; i < 10; i++)
            {
                var file = _driver.TakeScreenshot();
                Assert.That(File.Exists(file));
                File.Delete(file);
            }
        }

        [Test]
        public void TakeScreenshotTestWhenFileNameIsPassed()
        {
            var file = _driver.TakeScreenshot("screenfromtest");
            Assert.That(File.Exists(file));
            File.Delete(file);
        }

        [Test]
        public void TakeScreenshotTestWhenFilePathIsPassed()
        {
            var file = _driver.TakeScreenshot("screenfromtest", _defaultScreenshotsPath);
            Assert.That(File.Exists(file));
            File.Delete(file);
        }

        [Test]
        public void TakeScreenshotTestWhenFileFormatIsPassed()
        {
            var file = _driver.TakeScreenshot("screenfromtest", _defaultScreenshotsPath, "jpg");
            Assert.That(File.Exists(file));
            File.Delete(file);
        }

        [Test]
        public void TakeScreenshotTestWithoutParameters()
        {
            var file = _driver.TakeScreenshot();
            Assert.That(File.Exists(file));
            File.Delete(file);
        }

        [Test]
        public void TakeScreenshotAsScreenshotDoesntThrow()
        {
            Assert.DoesNotThrow(() => _driver.TakeScreenshotAsScreenshot());
        }

        [Test]
        public void TakeScreenshotAsScreenshotTestType()
        {
            Assert.True(typeof(Screenshot) == _driver.TakeScreenshotAsScreenshot().GetType());
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.CloseAndDispose();
        }
    }
}