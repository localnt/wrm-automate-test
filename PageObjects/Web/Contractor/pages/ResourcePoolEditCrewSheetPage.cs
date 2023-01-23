using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.BaseElements;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.pages
{
    public class ResourcePoolEditCrewSheetPage : BaseLoggedPage
    {

        [FindsBy(How = How.XPath, Using = "//div[(@class='Main-SpaceTop') and (./table[@id='ASPxPanel2_ContentPlaceHolder1_timesheet_officer_ASPxComboBox'])]")]
        private IWebElement timeSheetLastSubmitterMenuRootElemnt;

        [FindsBy(How = How.XPath, Using = "//div[(@class='Main-SpaceTop') and (./table[@id='ASPxPanel2_ContentPlaceHolder1_expense_officer_ASPxComboBox'])]")]
        private IWebElement expenceLastSubmitterMenuRootElemnt;

        public ResourcePoolEditCrewSheetPage(BaseInformation baseInformation) : base(baseInformation, new ReportUtils(baseInformation, "Resource Pool - Edit Crew Sheet", "page"))
        {
        }

        public void SelectTimeSheetLastSubmitter(String name)
        {
            GetReportUtils().AllureStepWithPageObject("Select Time Sheet Last Submitter user with name: " + name);
            new StandardDropdownMenu(GetBaseInformation(), timeSheetLastSubmitterMenuRootElemnt, new ReportUtils(GetBaseInformation(), "Time Sheet Last Submitter", "dropdown menu")).SelectMenuElement(name);
        }

        public void SelectExpenceLastSubmitter(String name)
        {
            GetReportUtils().AllureStepWithPageObject("Select Expense Last Submitter user with name: " + name);
            new StandardDropdownMenu(GetBaseInformation(), expenceLastSubmitterMenuRootElemnt, new ReportUtils(GetBaseInformation(), "Expense Last Submitter", "dropdown menu")).SelectMenuElement(name);
        }

    }
}
