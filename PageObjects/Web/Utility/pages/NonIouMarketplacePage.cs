using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.BaseElements;
using WRMAutotests.PageObjects.Web.Utility.panel;
using WRMAutotests.PageObjects.Web.Utility.windows;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.pages
{
    public class NonIouMarketplacePage : BaseLoggedPage
    {

        [FindsBy(How = How.CssSelector, Using = "table.dxeValidDynEditorTable.dxeRoot_DevEx[id*='HomeCityState']")]
        private IWebElement locationDropdownMenu;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_btnSearchContractor")]
        private IWebElement updateButton;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_btnProcureCrew")]
        private IWebElement procureCrewsButton;

        public NonIouMarketplacePage(BaseInformation baseInformation) : base(baseInformation, new ReportUtils(baseInformation, "Non-IOU Marketpalce", "page"))
        {
        }

        public CrewAvailabilitiesPanel GetCrewAvailabilitiesPanel()
        {
            return new CrewAvailabilitiesPanel(GetBaseInformation());
        }

        public StandardDropdownMenu GetLocationDropDownMenu()
        {
            return new StandardDropdownMenu(GetBaseInformation(), locationDropdownMenu, new ReportUtils(GetBaseInformation(), "Location", "Dropdown menu"));
        }

        public NonIouMarketplacePage ClickUpdateButton()
        {
            GetReportUtils().ClickButton("Update");
            GetWebElementUtils().clickWebElement(updateButton);
            Thread.Sleep(5000);
            return this;
        }

        public ProcureContractorCrewWindow ClickProcureCrew()
        {
            GetReportUtils().ClickButton("Procure Crew");
            GetWebElementUtils().clickWebElement(procureCrewsButton);
            Thread.Sleep(3000);
            GetWaitUtils().WaitForElementInvisible(By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_ASPxLoadingPanel1"));
            Thread.Sleep(3000);
            return new ProcureContractorCrewWindow(GetBaseInformation());
        }



    }
}
