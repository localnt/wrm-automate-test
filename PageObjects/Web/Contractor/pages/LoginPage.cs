using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.pages
{
    public class LoginPage : BasePage
    {

        [FindsBy(How = How.CssSelector, Using = "input[name*='$Login1$Username']")]
        private IWebElement loginField;

        [FindsBy(How = How.CssSelector, Using = "input[name*='$Login1$Password']")]
        private IWebElement passwordField;

        [FindsBy(How = How.CssSelector, Using = "div[id*='LoginButton']")]
        private IWebElement loginButton;


        public LoginPage(BaseInformation baseInformation) : base(baseInformation, new WRMAutotests.Utility.ReportUtils(baseInformation, "Contractor Login", "page"))
        {

        }

        public void EnterEmail(string email)
        {
            GetReportUtils().EnterValueToField("Email", email);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(loginField, email);
        }

        public void EnterPassword(string password)
        {
            GetReportUtils().EnterValueToField("Password", password);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(passwordField, password);
        }

        public WRMAutotests.PageObjects.Web.Contractor.pages.MainPage ClickLoginButton()
        {
            GetReportUtils().ClickButton("Login");
            GetWebElementUtils().clickWebElement(loginButton);
            return new MainPage(GetBaseInformation());
        }

    }
}
