using WRMAutotests.PageObjects.Web.Contractor.panels;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.pages
{
    public class ProcurementRequestPage : BaseLoggedPage
    {
        public ProcurementRequestPage(BaseInformation baseInformation) : base(baseInformation, new ReportUtils(baseInformation, "Procurement Request", "page"))
        {
        }

        public ProcurementRequestsPanel GetProcurementRequestsPanel()
        {
            return new ProcurementRequestsPanel(GetBaseInformation());
        }



    }
}
