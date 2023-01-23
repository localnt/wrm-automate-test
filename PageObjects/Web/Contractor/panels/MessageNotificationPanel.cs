using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.panels
{
    public class MessageNotificationPanel : BasePageObject
    {

        private static By rootLocator = By.CssSelector("#divMain-MessageNotificationContainer .Main-MessageSuccess");

        [FindsBy(How = How.CssSelector, Using = "#TopPanel_notifyDiv_Content")]
        private IWebElement messageText;

        [FindsBy(How = How.CssSelector, Using = "a")]
        private IWebElement linkElement;

        public MessageNotificationPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Message Notification", "panel"))
        {

        }

        public String GetNotificationText()
        {
            return messageText.Text;
        }

        public void ClickOnPresentLink()
        {
            GetReportUtils().ClickButton("Link");
            linkElement.Click();
        }

    }
}
