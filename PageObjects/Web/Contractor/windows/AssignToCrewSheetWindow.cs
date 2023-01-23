using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.BaseElements;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.windows
{
    public class AssignToCrewSheetWindow : BasePageObject
    {

        private static By rootLocator = By.XPath(".//div[contains(@id, 'ASPxPanel2_ContentPlaceHolder1_cpnBucketOrg_pucAssignToCrewsheet')]");

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnBucketOrg_pucAssignToCrewsheet_cbAssignToCrewsheet_ET")]
        private IWebElement dropdownRootElement;

        [FindsBy(How = How.CssSelector, Using = "div[title='Assign to Crew Sheet']")]
        private IWebElement assignToCrewSheetButton;

        public AssignToCrewSheetWindow(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Assign to Crew Sheet", "window"))
        {
        }

        public void SelectCrewSheet(String crewSheet)
        {
            new StandardDropdownMenu(GetBaseInformation(), dropdownRootElement, new ReportUtils(GetBaseInformation(), "Crew Sheet", "dropdown menu")).SelectMenuElement(crewSheet);
        }

        public void ClickAssignToCrewSheetButton()
        {
            GetReportUtils().ClickButton("Assign to Crew Sheet");
            GetWebElementUtils().clickWebElement(assignToCrewSheetButton);
            Thread.Sleep(3000);
            GetWaitUtils().WaitForLoadingPanelAbsent();
            Thread.Sleep(8000);
        }

        public void AssignToCrewSheet(String crewSheet)
        {
            SelectCrewSheet(crewSheet);
            ClickAssignToCrewSheetButton();
        }




    }
}
