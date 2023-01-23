using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Contractor.windows;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.pages
{
    public class ResourcePoolEditPage : BaseLoggedPage
    {

        [FindsBy(How = How.CssSelector, Using = "div[title='Delete this resource pool']")]
        private IWebElement deleteButton;

        public ResourcePoolEditPage(BaseInformation baseInformation) : base(baseInformation, new ReportUtils(baseInformation, "Resource pool edit", "page"))
        {
        }

        public ConfirmationWindow ClickDeleteButton()
        {
            GetReportUtils().ClickButton("Delete");
            GetWebElementUtils().clickWebElement(deleteButton);
            return new ConfirmationWindow(GetBaseInformation());

        }



    }
}
