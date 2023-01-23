using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Utility.panel;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.pages
{
    public class BaseLoggedPage : BasePage
    {
        public BaseLoggedPage(BaseInformation baseInformation, ReportUtils reportUtils) : base(baseInformation, reportUtils)
        {



        }

        public WRMAutotests.PageObjects.Web.Utility.panel.HeaderPanel GetHeaderPanel()
        {
            return new WRMAutotests.PageObjects.Web.Utility.panel.HeaderPanel(GetBaseInformation());
        }

        public MessageNotificationPanel GetMessageNotificationPanel()
        {
            return new MessageNotificationPanel(GetBaseInformation());
        }


    }
}
