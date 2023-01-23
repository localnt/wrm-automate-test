using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.windows
{
    public class CrewSheetOrganizationWindow : BasePageObject
    {

        private static By rootLocator = By.CssSelector("div[id*='cpnCrewSheetOrg'].dxpcLite_DevEx");

        [FindsBy(How = How.CssSelector, Using = "div.dxpc-closeBtn")]
        private IWebElement closeButton;

        public CrewSheetOrganizationWindow(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Crew Sheet Organization", "window"))
        {
        }

        public void ClickCloseButton()
        {
            GetReportUtils().ClickButton("Close");
            GetWebElementUtils().clickWebElement(closeButton);
            Thread.Sleep(10000);
        }

    }
}
