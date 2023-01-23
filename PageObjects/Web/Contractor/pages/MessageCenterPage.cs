using WRMAutotests.PageObjects.Web.Contractor.panels;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.pages
{
    public class MessageCenterPage : BaseLoggedPage
    {

        public MessageCenterPage(BaseInformation baseInformation) : base(baseInformation, new ReportUtils(baseInformation, "Message Center", "page"))
        {

        }

        public MessageCenterPanel GetMessageCenterPanel()
        {
            Thread.Sleep(5000);
            return new MessageCenterPanel(GetBaseInformation());
        }

    }
}
