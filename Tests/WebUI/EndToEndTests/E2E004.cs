using RetryOnException;
using WRMAutotests.PageObjects.Web.Contractor.pages;

namespace WRMAutotests.Tests.WebUI.EndToEndTests
{
    public class E2E004 : BaseEndToEndTest
    {

        [RetryOnException(ListOfExceptions = new[] { typeof(Exception) })]
        [Retry(numberOfTryFroWebTests)]
        [Test]
        public void E2E004_Test()
        {
            WRMAutotests.PageObjects.Web.Contractor.pages.LoginPage loginPage = OpenLoginPageForContractor();
            loginPage.EnterEmail(contractorUser.GetEmail());
            loginPage.EnterPassword(contractorUser.GetPassword());
            MainPage mainPage = loginPage.ClickLoginButton();

            mainPage.GetAssertionUtils().TrueAssertion("Verify that Header present", WRMAutotests.PageObjects.Web.Contractor.panels.HeaderPanel.IsHeaderPanelPresent(GetDefaultBaseInformation()));
        }

    }
}
