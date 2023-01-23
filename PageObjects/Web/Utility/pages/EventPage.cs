using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.BaseElements;
using WRMAutotests.PageObjects.Web.Utility.windows;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.pages
{
    public class EventPage : WRMAutotests.PageObjects.Web.Utility.pages.BaseLoggedPage
    {
        [FindsBy(How = How.CssSelector, Using = "input[id*='_code_']")]
        private IWebElement codeField;

        [FindsBy(How = How.CssSelector, Using = "input[id*='name']")]
        private IWebElement nameField;

        [FindsBy(How = How.CssSelector, Using = "table[id*='_event_type_']")]
        private IWebElement eventTypeSelectorRoot;

        [FindsBy(How = How.CssSelector, Using = "textarea[id*='_evt_description_']")]
        private IWebElement descriptionTextArea;

        [FindsBy(How = How.CssSelector, Using = "table[id*='OperatingCompany']")]
        private IWebElement operatingCompanySelectorRoot;

        [FindsBy(How = How.CssSelector, Using = "table[id*='evt_default_event'] span.dxICheckBox_DevEx")]
        private IWebElement operatingCompanyCheckbox;

        [FindsBy(How = How.CssSelector, Using = "div[title='Add the information']")]
        private IWebElement addButton;

        [FindsBy(How = How.CssSelector, Using = "div[title='Delete the information']")]
        private IWebElement cancelEvent;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_evt_event_start_ASPxDateEdit_B-1")]
        private IWebElement eventStartDropdownMenu;

        [FindsBy(How = How.CssSelector, Using = "div#ASPxPanel2_ContentPlaceHolder1_evt_event_start_ASPxDateEdit_DDD_PW-1")]
        private IWebElement eventStartDateMenuBase;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_evt_event_end_ASPxDateEdit_B-1")]
        private IWebElement eventEndDropdownMenu;

        [FindsBy(How = How.CssSelector, Using = "div#ASPxPanel2_ContentPlaceHolder1_evt_event_end_ASPxDateEdit_DDD_PW-1")]
        private IWebElement eventEndDateMenuBase;

        public EventPage(BaseInformation baseInformation) : base(baseInformation, new WRMAutotests.Utility.ReportUtils(baseInformation, "Event", "page"))
        {

        }

        public void EnterCode(String code)
        {
            GetReportUtils().EnterValueToField("Code", code);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(codeField, code);
        }

        public void EnterName(String name)
        {
            GetReportUtils().EnterValueToField("Name", name);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(nameField, name);
        }

        public void SelectTypeOfEvent(String eventType)
        {
            new StandardDropdownMenu(GetBaseInformation(), eventTypeSelectorRoot, new WRMAutotests.Utility.ReportUtils(GetBaseInformation(), "Select type of Event", "dropdown menu")).SelectMenuElement(eventType);
        }

        public void EnterDescription(String description)
        {
            GetReportUtils().EnterValueToField("Description", description);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(descriptionTextArea, description);
        }

        public void SelectOperatingCompany(String operatingCompany)
        {
            GetReportUtils().AllureStepWithPageObject("Select Operating company: " + operatingCompany);
            //open dropdown menu
            operatingCompanySelectorRoot.FindElement(By.CssSelector("td[onmousedown*='DDDropDown']"))
                .Click();

            //click to the target element
            operatingCompanySelectorRoot.FindElements(By.CssSelector("td.dxeT[id*='DDD_ltbOperatingCompany']")).First(e => e.Text.Equals(operatingCompany)).Click();

            //click close button
            operatingCompanySelectorRoot.FindElement(By.CssSelector("div[id*='OperatingCompany_DDD_btnCompany_0']"))
                .Click();
            Thread.Sleep(2000);
        }

        public Boolean IsDefaultEventChecked()
        {
            return operatingCompanyCheckbox.GetAttribute("class").Contains("CheckBoxChecked");
        }

        public void checkDefaultEventCheckbox()
        {
            GetReportUtils().AllureStepWithPageObject("Check default event Checkbox");
            if (IsDefaultEventChecked())
                return;
            else
                GetWebElementUtils().clickWebElement(operatingCompanyCheckbox);
        }

        public ConfirmationWindow clickAddButton()
        {
            GetReportUtils().ClickButton("Add");
            GetWebElementUtils().clickWebElement(addButton);
            return new ConfirmationWindow(GetBaseInformation());
        }

        public ConfirmationWindow ClickCancelEvent()
        {
            GetReportUtils().ClickButton("Cancel");
            GetWebElementUtils().clickWebElement(cancelEvent);
            return new ConfirmationWindow(GetBaseInformation());
        }

        public void SelectEventStartDate(DateTime dateTime)
        {
            GetReportUtils().AllureStepWithPageObject("Select Event start date");
            eventStartDropdownMenu.Click();
            Thread.Sleep(2000);
            new DateTimeSelectorWindow(GetBaseInformation(), eventStartDateMenuBase).SelectDate(dateTime);
        }

        public void SelectEventEndDate(DateTime dateTime)
        {
            GetReportUtils().AllureStepWithPageObject("Select Event end date");
            eventEndDropdownMenu.Click();
            Thread.Sleep(2000);
            new DateTimeSelectorWindow(GetBaseInformation(), eventEndDateMenuBase).SelectDate(dateTime);
        }


    }
}
