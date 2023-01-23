using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace WRMAutotests.Utility.Web
{
    public class WaitUtils
    {

        private IWebDriver driver;
        private WebDriverWait wait;
        private int DEFAULT_EXPLICITY_WAIT_SECONDS = 120;

        public WaitUtils(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DEFAULT_EXPLICITY_WAIT_SECONDS));
        }

        public void WaitForElementPresentByLocator(By locator)
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
        }

        public void WaitForElementInvisible(By locator)
        {
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public IWebElement WaitForElementAtrivuteEqual(IWebElement webElement, String attributeName, String attributeValue)
        {
            int sizeOfWaitInSeconds = 1;
            for (int i = 0; i < (int)DEFAULT_EXPLICITY_WAIT_SECONDS / sizeOfWaitInSeconds; i++)
            {
                if (webElement.GetAttribute(attributeName).Equals(attributeValue))
                {
                    return webElement;
                }
                else
                {
                    Thread.Sleep(sizeOfWaitInSeconds * 1000);
                }
            }
            throw new AssertionException("For target Web element attribute " + attributeName + " equeal " + webElement.GetAttribute(attributeName) + ", instead expected value: " + attributeValue);
        }

        public IWebElement WaitForElementClicable(IWebElement webElement)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement));
            return webElement;
        }

        public void waitForElementAbsent(IWebElement element)
        {
            for (int i = 0; i < 50; i++)
            {
                try
                {
                    if (element.Displayed)
                    {
                        Thread.Sleep(2000);
                    }
                }
                catch (Exception ex)
                {
                    Thread.Sleep(2000);
                    return;
                }
            }
            throw new AssertionException("Element still visible");
        }

        public void WaitForAllListElementsPresent(IList<IWebElement> elements)
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(new ReadOnlyCollection<IWebElement>(elements)));
        }

        public void WaitForAllElementsInvisible(By locatorOfCollection)
        {
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
                    fluentWait.Timeout = TimeSpan.FromSeconds(6);
                    fluentWait.PollingInterval = TimeSpan.FromMilliseconds(1000);
                    fluentWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locatorOfCollection));
                }
                catch (Exception ex)
                {
                    return;
                }
            }
            throw new AssertionException("Collection of elements with locator " + locatorOfCollection + " present on the page");
        }

        public void WaitForLoadingPanelAbsent()
        {
            Thread.Sleep(2000);
            WaitForAllElementsInvisible(By.CssSelector(".dxgvLoadingPanel_DevEx"));
            Thread.Sleep(2000);
        }

    }


}
