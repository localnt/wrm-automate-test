using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WRMAutotests.Utility.Web
{
    public class DriverUtils
    {

        private int DEFAULT_IMPLICIT_WAIT_SECONDS = 240;

        public IWebDriver GenerateDefaultWebDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            IWebDriver driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(5));
            AddDefaultSettingsForDriver(driver);
            return driver;
        }

        public void AddDefaultSettingsForDriver(IWebDriver driver)
        {
            driver.Manage().Window
                .Maximize();
            driver.Manage()
                .Timeouts()
                .ImplicitWait = System.TimeSpan.FromSeconds(DEFAULT_IMPLICIT_WAIT_SECONDS);
        }

        static public void CloseDriver(IWebDriver driver)
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }


    }
}
