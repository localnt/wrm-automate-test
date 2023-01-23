using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.windows.crewavailabilitywindow
{
    public class CrewAvailabilityFormWindow : BasePageObject
    {

        private static By rootLocator = By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_cbpIntenalCrewLayout_pucCrewAvaiabilityForm_PW-1");

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cbpIntenalCrewLayout_pucCrewAvaiabilityForm_cbCrewAvaiabilityForm_gvCrewAvaiabilityForm")]
        private IWebElement resourcePoolPanelRoot;

        [FindsBy(How = How.CssSelector, Using = "div#divNonIOUSelectUtilityContract")]
        private IWebElement utilityNameRoot;

        [FindsBy(How = How.XPath, Using = ".//td[.//span[text()='Share Crew Availability']]")]
        private IWebElement shareCrewAvailabilityButton;

        public CrewAvailabilityFormWindow(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Crew Availability Form", "window"))
        {
        }

        public ResourcePoolTablePanel GetResourcePoolTablePanel()
        {
            return new ResourcePoolTablePanel(GetBaseInformation(), resourcePoolPanelRoot);
        }

        public UtilitiesTablePanel GetUtilitiesTablePanel()
        {
            return new UtilitiesTablePanel(GetBaseInformation(), utilityNameRoot);
        }

        public void ClickShareCrewAvailability()
        {
            GetReportUtils().ClickButton("Share Crew Availability");
            GetWebElementUtils().clickWebElement(shareCrewAvailabilityButton);
            Thread.Sleep(3000);
            GetWaitUtils().WaitForElementInvisible(By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_ASPxLoadingPanel1"));
            Thread.Sleep(3000);
        }

    }
}
