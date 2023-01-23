using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.BaseElements;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.windows
{
    public class AssignCrewSheetToUtilityWindow : BasePageObject
    {

        private static By rootLocator = By.CssSelector("div[id*='ContentPlaceHolder1_cpnSubmitCrewSheet_pucCrewSheetSubmit']");

        [FindsBy(How = How.CssSelector, Using = "table.dxeValidDynEditorTable.dxeRoot_DevEx")]
        private IWebElement utilitySelectorRootElement;

        [FindsBy(How = How.CssSelector, Using = "div[id*='SubmitCrewSheetToUtility']")]
        private IWebElement submitCrewSheetButton;

        public AssignCrewSheetToUtilityWindow(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Assign Crew Sheets to Utility", "window"))
        {
        }

        public void SelectUtilityFromDropDownMenu(String utility)
        {
            GetWaitUtils().WaitForLoadingPanelAbsent();
            new StandardDropdownMenu(GetBaseInformation(), utilitySelectorRootElement, new ReportUtils(GetBaseInformation(), "Utility", "dropdown menu")).SelectMenuElement(utility);
        }

        public ConfirmationWindow ClickSubmitCrewSheet()
        {
            GetReportUtils().ClickButton("Submit Crew Sheet");
            GetWebElementUtils().clickWebElement(submitCrewSheetButton);
            return new ConfirmationWindow(GetBaseInformation(), By.CssSelector("div[id='popupMaster_Confirmation_PW-1']"));
        }

        public ConfirmationWindow SelectUtility(String utility)
        {
            SelectUtilityFromDropDownMenu(utility);
            return ClickSubmitCrewSheet();
        }



    }
}
