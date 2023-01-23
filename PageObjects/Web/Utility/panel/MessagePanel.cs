using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.panel
{
    public class MessagePanel : BasePageObject
    {

        private static By rootLocator = By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_pcMainTab_CC");


        [FindsBy(How = How.CssSelector, Using = "#divMessageBody > span")]
        private IWebElement bodyText;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pcMainTab_cpnInbox_ViewMessage_lbMessageFromTo_Recieved")]
        private IWebElement dateLabel;

        public MessagePanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Message", "panel"))
        {
        }

        public String GetMessageText()
        {
            return bodyText.Text;
        }

        public DateTime GetDate()
        {
            return DateTime.ParseExact(dateLabel.Text, "MM/dd/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
        }


    }
}
