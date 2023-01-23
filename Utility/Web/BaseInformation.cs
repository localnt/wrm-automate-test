using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WRMAutotests.Utility.Web
{
    public class BaseInformation
    {
        private IWebDriver driver;
        private Boolean makeScreenshootForEveryStep = false;

        public BaseInformation(IWebDriver driver)
        {
            this.driver = driver;
        }

        public BaseInformation(IWebDriver driver, Boolean makeScreenshootForEveryStep)
        {
            this.driver = driver;
            this.makeScreenshootForEveryStep = makeScreenshootForEveryStep;
        }

        public IWebDriver GetDriver()
        {
            return driver;
        }

        public Boolean makeScreenshootEveryStep()
        {
            return makeScreenshootForEveryStep;
        }

        public String GetSessionId()
        {
            return ((ChromeDriver)driver).SessionId.ToString().Substring(0, 6);
        }

    }
}
