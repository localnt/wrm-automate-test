using RetryOnException;
using WRMAutotests.PageObjects.Web.Utility.pages;

namespace WRMAutotests.Tests.WebUI.EndToEndTests
{
    public class E2E001 : BaseEndToEndTest
    {

        private String operatingCompanyName = excelReadedUtils.GetCellValue(1, 3, 2).Equals("") ? defaultOperatingCompany : excelReadedUtils.GetCellValue(1, 3, 2);
        private String eventName = excelReadedUtils.GetCellValue(1, 3, 3).Equals("") ? defaultEvent : excelReadedUtils.GetCellValue(1, 3, 3);
        private String discipline = excelReadedUtils.GetCellValue(1, 3, 4).Equals("") ? defaultDiscipline : excelReadedUtils.GetCellValue(1, 3, 4);


        [RetryOnException(ListOfExceptions = new[] { typeof(Exception) })]
        [Retry(numberOfTryFroWebTests)]
        [Test]
        public void E2E001_Test()
        {
            DefaultPage defaultPage = OpenDefaultUtilityPage();
            WRMAutotests.PageObjects.Web.Utility.pages.LoginPage loginPage = defaultPage.ClickLoginButton();
            loginPage.GetLoginPanel().EnterEmail(utilityUser.GetEmail());
            loginPage.GetLoginPanel().EnterPassword((utilityUser.GetPassword()));
            EventSelectionPage eventSelectionPage = loginPage.GetLoginPanel().ClickLoginButton();
            eventSelectionPage.GetWelcomeToStormManagerPanel().SelectOperatingCompany(operatingCompanyName);
            eventSelectionPage.GetWelcomeToStormManagerPanel().SelectEvent(eventName);

            eventSelectionPage.GetAssertionUtils().TrueAssertion("Verify that Header panel present", WRMAutotests.PageObjects.Web.Utility.panel.HeaderPanel.IsHeaderPanelPresent(GetDefaultBaseInformation()));
        }

    }
}
