using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Contractor.panels;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.pages
{
    public class ResourcePoolEditOrganizationPage : BasePageObject
    {


        public ResourcePoolEditOrganizationPage(BaseInformation baseInformation) : base(baseInformation, new ReportUtils(baseInformation, "Resource Pool - Edit Organization", "page"))
        {
        }

        public ResourcePoolEditOrganizationPanel GetResourcePoolEditOrganizationPanel()
        {
            return new ResourcePoolEditOrganizationPanel(GetBaseInformation());
        }


    }
}
