using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.pages
{
    public class DefaultPage : BasePage
    {

        [FindsBy(How = How.XPath, Using = ".//div[./span[normalize-space(text())='Login']]")]
        private IWebElement loginButton;

        public DefaultPage(BaseInformation baseInformation) : base(baseInformation, new WRMAutotests.Utility.ReportUtils(baseInformation, "Utility Default", "page"))
        {
        }

        public LoginPage ClickLoginButton()
        {
            GetReportUtils().ClickButton("Login");
            GetWebElementUtils().clickWebElement(loginButton);
            return new LoginPage(GetBaseInformation());
        }


    }
}
