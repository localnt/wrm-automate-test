using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.pages
{
    public class ProcurementResponsePage : BaseLoggedPage
    {
        [FindsBy(How = How.CssSelector, Using = "div[title='Accept']")]
        private IWebElement acceptButton;

        public ProcurementResponsePage(BaseInformation baseInformation) : base(baseInformation, new ReportUtils(baseInformation, "Procurement Responce", "page"))
        {
        }

        public ConfirmationWindow ClickAcceptButton()
        {
            GetReportUtils().ClickButton("Accept");
            GetWebElementUtils().clickWebElement(acceptButton);
            return new ConfirmationWindow(GetBaseInformation());
        }


        public class ConfirmationWindow : BasePageObject
        {

            private static By rootLocator = By.CssSelector("#popupMaster_Confirmation_PW-1");

            [FindsBy(How = How.CssSelector, Using = "div[id*='Confirmation_confirm']")]
            private IWebElement confirmationButton;

            public ConfirmationWindow(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Confirmation", "window"))
            {
            }

            public void ClickConfirmButton()
            {
                GetReportUtils().ClickButton("Confirm");
                GetWebElementUtils().clickWebElement(confirmationButton);
                Thread.Sleep(2000);
                GetWaitUtils().WaitForElementInvisible(By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_ASPxLoadingPanel1"));

            }


        }


    }
}
