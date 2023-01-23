using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Utility.panel;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.pages
{
    public class EventsPage : BaseLoggedPage
    {

        [FindsBy(How = How.CssSelector, Using = "div[title='Add a new record']")]
        private IWebElement addButton;

        public EventsPage(BaseInformation baseInformation) : base(baseInformation, new WRMAutotests.Utility.ReportUtils(baseInformation, "Utility Events", "page"))
        {
        }

        public EventsPanel GetEventsPanel()
        {
            Thread.Sleep(10000);
            return new EventsPanel(GetBaseInformation());
        }

        public EventPage clickAddButton()
        {
            GetReportUtils().ClickButton("Add");
            GetWebElementUtils().clickWebElement(addButton);
            return new EventPage(GetBaseInformation());
        }



    }
}
