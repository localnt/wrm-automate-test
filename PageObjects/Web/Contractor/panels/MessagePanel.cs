using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.panels
{
    public class MessagePanel : BasePageObject
    {

        private static By rootLocator = By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_pcMainTab_CC");


        [FindsBy(How = How.CssSelector, Using = "#divMessageBody > span")]
        private IWebElement bodyText;

        public MessagePanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Message", "panel"))
        {
        }

        public String GetMessageText()
        {
            return bodyText.Text;
        }


    }
}
