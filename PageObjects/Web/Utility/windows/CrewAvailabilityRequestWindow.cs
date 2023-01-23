using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.windows
{
    public class CrewAvailabilityRequestWindow : BasePageObject
    {

        private static By rootLocator = By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_cpnAskContractor_popupAskContractor_PW-1");

        [FindsBy(How = How.CssSelector, Using = "input[id*='cbpPerformRequesting_txtSubject_I']")]
        private IWebElement subjectInput;

        [FindsBy(How = How.CssSelector, Using = "textarea[id*='Contractor_popupAskContractor_cbpPerformRequesting']")]
        private IWebElement messageTextarea;

        [FindsBy(How = How.CssSelector, Using = "div[id*='btnPopupContractorRequest']")]
        private IWebElement clickRequestCrewAvailabilityInfoButton;

        public CrewAvailabilityRequestWindow(BaseInformation baseInformation) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Crew Availability Reqiest", "window"))
        {
        }

        public void EnterSubject(String subject)
        {
            GetReportUtils().EnterValueToField("Subject", subject);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(subjectInput, subject);
        }

        public void EnterMessage(String message)
        {
            GetReportUtils().EnterValueToField("Message", message);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(messageTextarea, message);
        }

        public void ClickRequestCrewAvailabilityInfo()
        {
            GetReportUtils().ClickButton("Request Crew Availability");
            GetWebElementUtils().clickWebElement(clickRequestCrewAvailabilityInfoButton);
        }


    }
}
