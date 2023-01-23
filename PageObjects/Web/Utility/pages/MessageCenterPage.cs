using WRMAutotests.PageObjects.Web.Utility.panel;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.pages
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
