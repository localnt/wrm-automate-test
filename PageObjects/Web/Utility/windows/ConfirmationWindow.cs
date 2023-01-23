using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.windows
{
    public class ConfirmationWindow : BasePageObject
    {

        private static By rootLocator = By.CssSelector("div[id*='popupMaster'].dxpc-mainDiv");

        [FindsBy(How = How.CssSelector, Using = "div[id*='btnMasterPopupConfirm']")]
        private IWebElement confirmButton;

        public ConfirmationWindow(BaseInformation baseInformation) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Confirmation", "window"))
        {
        }

        public void ClickConfirmButton()
        {
            GetReportUtils().ClickButton("Confirm");
            GetWebElementUtils().clickWebElement(confirmButton);

        }



    }
}
