using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.panels
{
    public class MessageCenterPanel : BasePageObject
    {

        private static By rootLocator = By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_pcMainTab_CC");

        [FindsBy(How = How.CssSelector, Using = "tr.dxgvDataRow_DevEx")]
        private IList<IWebElement> emailRows;

        public MessageCenterPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Message center", "panel"))
        {
        }

        public IList<EmailRow> GetEmailRows()
        {
            IList<EmailRow> result = new List<EmailRow>();
            foreach (IWebElement emailRow in emailRows)
            {
                result.Add(new EmailRow(GetBaseInformation(), emailRow));
            }
            return result;
        }

        public IList<EmailRow> GetEmailRowsByPartSubject(String partOfSubject)
        {
            return GetEmailRows().Where(r => r.GetSubject().Contains(partOfSubject)).ToList();
        }

        public class EmailRow : BasePageObject
        {

            [FindsBy(How = How.CssSelector, Using = "span[id*='ASPxPanel2_ContentPlaceHolder1_pcMainTab_cpnInbox_gvInbox_cell']")]
            private IWebElement subjectText;

            [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pcMainTab_cpnInbox_ViewMessage_LPV")]
            private IWebElement loadingPanelForOpeningEmail;

            public EmailRow(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Message", "row"))
            {
            }

            public String GetSubject()
            {
                return subjectText.Text;
            }

            public MessagePanel ClickSubject()
            {
                subjectText.Click();
                GetWaitUtils().waitForElementAbsent(loadingPanelForOpeningEmail);
                Thread.Sleep(3000);
                return new MessagePanel(GetBaseInformation());
            }

        }

    }
}
