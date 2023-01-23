using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Contractor.panels;
using WRMAutotests.PageObjects.Web.Contractor.windows;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.pages
{
    public class ResourcePoolOverviewPage : BaseLoggedPage
    {

        [FindsBy(How = How.CssSelector, Using = "div[id*='btnEditBucketOrg']")]
        private IWebElement editOrganizationButton;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnCrewSheet_btnNewCrewSheet")]
        private IWebElement newCrewSheetButton;

        [FindsBy(How = How.XPath, Using = ".//div[contains(@id, 'btnCrewSheetSubmitTo')]")]
        private IWebElement submitButton;

        public ResourcePoolOverviewPage(BaseInformation baseInformation) : base(baseInformation, new ReportUtils(baseInformation, "Resource pool owerview", "page"))
        {
        }

        public ResourcePoolEditOrganizationPage ClickEditOrganizationButton()
        {
            GetReportUtils().ClickButton("Edit Organization");
            GetWebElementUtils().clickWebElement(editOrganizationButton);
            return new ResourcePoolEditOrganizationPage(GetBaseInformation());
        }

        public void ClickNewCrewSheetButton()
        {
            GetReportUtils().ClickButton("New Crew Sheet");
            GetWebElementUtils().clickWebElement(newCrewSheetButton);
            GetWaitUtils().WaitForLoadingPanelAbsent();
            Thread.Sleep(3000);
            GetWaitUtils().WaitForLoadingPanelAbsent();
        }

        public CrewSheetsPanel GetCrewSheetsPanel()
        {
            return new CrewSheetsPanel(GetBaseInformation());
        }

        public ResourcePoolResourcesPanel GetResourcePoolResourcesPanel()
        {
            return new ResourcePoolResourcesPanel(GetBaseInformation());
        }

        public AssignCrewSheetToUtilityWindow ClickSubmitButton()
        {
            GetReportUtils().ClickButton("Submit");
            GetWebElementUtils().clickWebElement(submitButton);
            GetWaitUtils().WaitForLoadingPanelAbsent();
            return new AssignCrewSheetToUtilityWindow(GetBaseInformation());
        }



    }
}
