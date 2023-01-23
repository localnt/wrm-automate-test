using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.windows
{
    public class ConfirmationWindow : BasePageObject
    {

        private static By rootLocator = By.CssSelector("div[id*='popupMaster'].dxpc-mainDiv");

        [FindsBy(How = How.CssSelector, Using = "div[id*='btnMasterPopupConfirm'],div[id*='btnpopupMaster_Confirmation_confirm']")]
        private IWebElement confirmButton;

        public ConfirmationWindow(BaseInformation baseInformation) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Confirmation", "window"))
        {
        }

        public ConfirmationWindow(BaseInformation baseInformation, By rootLocator) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Confirmation", "window"))
        {
        }

        public void ClickConfirmButton()
        {
            GetReportUtils().ClickButton("Confirm");
            GetWebElementUtils().clickWebElement(confirmButton);
            GetWaitUtils().WaitForLoadingPanelAbsent();
            Thread.Sleep(10000);
        }



    }
}
