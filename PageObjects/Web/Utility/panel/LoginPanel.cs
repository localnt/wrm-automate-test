using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.panel
{
    public class LoginPanel : BasePageObject
    {

        private static By rootLocator = By.CssSelector("div.card.inner_login");

        [FindsBy(How = How.CssSelector, Using = "#username")]
        private IWebElement usernameField;

        [FindsBy(How = How.CssSelector, Using = "#password")]
        private IWebElement passwordField;

        [FindsBy(How = How.CssSelector, Using = "#btn_VendorLogin")]
        private IWebElement loginButton;

        public LoginPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Login", "panel"))
        {
        }

        public void EnterEmail(String email)
        {
            GetReportUtils().EnterValueToField("Email", email);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(usernameField, email);
        }

        public void EnterPassword(String password)
        {
            GetReportUtils().EnterValueToField("Password", password);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(passwordField, password);
        }

        public EventSelectionPage ClickLoginButton()
        {
            GetReportUtils().ClickButton("Login");
            GetWebElementUtils().clickWebElement(loginButton);
            return new EventSelectionPage(GetBaseInformation());
        }

    }
}
