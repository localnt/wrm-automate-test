using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Utility.panel;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.pages
{
    public class EventSelectionPage : BasePage
    {


        public EventSelectionPage(BaseInformation baseInformation) : base(baseInformation, new WRMAutotests.Utility.ReportUtils(baseInformation, "Utility Event Selection", "page"))
        {
        }

        public WelcomeToStormManagerPanel GetWelcomeToStormManagerPanel()
        {
            return new WelcomeToStormManagerPanel(GetBaseInformation());
        }

    }
}
