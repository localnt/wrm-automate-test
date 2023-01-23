using OpenQA.Selenium;

namespace WRMAutotests.Utility.Web
{
    public class WebElementUtils
    {

        private IWebDriver webDriver;
        private WaitUtils waitUtils;

        public WebElementUtils(IWebDriver driver, WaitUtils waitUtils)
        {
            this.webDriver = driver;
            this.waitUtils = waitUtils;
        }

        public void clickWebElement(IWebElement button)
        {
            waitUtils.WaitForElementClicable(button).Click();
        }

        public void enterValueToFieldWithWaitEntering(IWebElement field, String text)
        {
            field.Clear();
            waitUtils.WaitForElementAtrivuteEqual(field, "value", "");
            field.SendKeys(text);
            waitUtils.WaitForElementAtrivuteEqual(field, "value", text);
        }

        public void enterValueToFieldWithoutWaitEntering(IWebElement field, String text)
        {
            field.Clear();
            Thread.Sleep(2000);
            field.SendKeys(text);
            Thread.Sleep(2000);
        }

        public Boolean IsWebElementPresent(IWebElement element)
        {
            TimeSpan oldImplicityWait = webDriver.Manage().Timeouts().ImplicitWait;
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            try
            {
                if (element.Displayed)
                {
                    webDriver.Manage().Timeouts().ImplicitWait = oldImplicityWait;
                    return true;
                }
                webDriver.Manage().Timeouts().ImplicitWait = oldImplicityWait;
                return true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                webDriver.Manage().Timeouts().ImplicitWait = oldImplicityWait;
            }
            return false;
        }

        public void ScrollToElement(IWebElement element)
        {
            var locatableElement = element as ILocatable;
            var pos = locatableElement.LocationOnScreenOnceScrolledIntoView;
            Thread.Sleep(500);
        }


    }
}
