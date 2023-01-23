using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.pages
{
    public class MainPage : BaseLoggedPage
    {

        public MainPage(BaseInformation baseInformation) : base(baseInformation, new WRMAutotests.Utility.ReportUtils(baseInformation, "Contractor main", "page"))
        {

        }

    }
}
