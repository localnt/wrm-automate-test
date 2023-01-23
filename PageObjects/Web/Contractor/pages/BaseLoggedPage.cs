using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Contractor.panels;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.pages
{
    public abstract class BaseLoggedPage : BasePage
    {

        public BaseLoggedPage(BaseInformation baseInformation, ReportUtils reportUtils) : base(baseInformation, reportUtils)
        {

        }

        public HeaderPanel GetHeaderPanel()
        {
            return new HeaderPanel(GetBaseInformation());
        }

        public MessageNotificationPanel GetMessageNotificationPanel()
        {
            return new MessageNotificationPanel(GetBaseInformation());
        }

    }
}
