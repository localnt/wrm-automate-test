using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Utility.panel;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(BaseInformation baseInformation) : base(baseInformation, new WRMAutotests.Utility.ReportUtils(baseInformation, "Utility Login", "page"))
        {
        }

        public WRMAutotests.PageObjects.Web.Utility.panel.LoginPanel GetLoginPanel()
        {
            return new LoginPanel(GetBaseInformation());
        }

    }
}
