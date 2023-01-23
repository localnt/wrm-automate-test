using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Utility.windows;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.pages
{
    public class CreawAvailabilityRequestPage : BaseLoggedPage
    {

        [FindsBy(How = How.CssSelector, Using = "div[title='Request Crew Availability Info ...']")]
        private IWebElement requestCreaAvailabilityInfoButton;

        [FindsBy(How = How.XPath, Using = ".//li/a/span[text()='Distribution Line']")]
        private IWebElement distributionLineButton;

        [FindsBy(How = How.CssSelector, Using = "ul.dxtc-strip > li.dxtc-tab")]
        private IList<IWebElement> disciplineTabsWithutActiveTab;

        [FindsBy(How = How.CssSelector, Using = "li.dxtc-activeTab:not([style*='display: none'])")]
        private IWebElement activeTab;

        public CreawAvailabilityRequestPage(BaseInformation baseInformation) : base(baseInformation, new WRMAutotests.Utility.ReportUtils(baseInformation, "Utility Creaw Availability Request", "page"))
        {
        }

        public WRMAutotests.PageObjects.Web.Utility.panel.CrewAvailabilityRequestContractorsTablePanel GetCrewAvailabilityRequestContractorsTablePanel()
        {
            Thread.Sleep(10000);
            return new WRMAutotests.PageObjects.Web.Utility.panel.CrewAvailabilityRequestContractorsTablePanel(GetBaseInformation());
        }

        public CreawAvailabilityRequestPage ClickTabByName(String discipline)
        {
            GetReportUtils().ClickButton("Click Tab with name " + discipline);

            //possible target tab already selected
            if (activeTab.FindElement(By.CssSelector("a > span")).Text.Trim().Equals(discipline))
            {
                return this;
            }
            else
            {
                IWebElement foundUnactiveTab = disciplineTabsWithutActiveTab.Where(e => e.FindElement(By.CssSelector("a > span")).Text.Trim().Equals(discipline))
                .First();
                GetWebElementUtils().clickWebElement(foundUnactiveTab);
                GetWaitUtils().WaitForLoadingPanelAbsent();
                return this;
            }

        }

        public CrewAvailabilityRequestWindow ClickRequestCrewAvailabilityInfo()
        {
            GetReportUtils().ClickButton("Request Crew Availability");
            GetWebElementUtils().clickWebElement(requestCreaAvailabilityInfoButton);
            Thread.Sleep(10000);
            return new CrewAvailabilityRequestWindow(GetBaseInformation());
        }

    }
}
