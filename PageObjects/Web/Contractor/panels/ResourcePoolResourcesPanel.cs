using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Contractor.windows;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.panels
{
    public class ResourcePoolResourcesPanel : BasePageObject
    {

        private static By rootLocator = By.CssSelector("div#ASPxPanel2_ContentPlaceHolder1_cpnBucketOrg");

        [FindsBy(How = How.CssSelector, Using = "tr.treeTypeSupportRow")]
        private IList<IWebElement> rows;

        [FindsBy(How = How.XPath, Using = ".//div[contains(@id, 'btnOpenAssignToCrewsheet')]")]
        private IWebElement assignButton;

        public ResourcePoolResourcesPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Resource Pool Resources", "panel"))
        {
        }

        public AssignToCrewSheetWindow ClickAssignButton()
        {
            GetReportUtils().ClickButton("Assign");
            GetWebElementUtils().clickWebElement(assignButton);
            return new AssignToCrewSheetWindow(GetBaseInformation());
        }

        public IList<Row> GetRows()
        {
            IList<Row> result = new List<Row>();
            foreach (IWebElement row in rows)
            {
                result.Add(new Row(GetBaseInformation(), row));
            }
            return result;
        }

        public class Row : BasePageObject
        {

            [FindsBy(How = How.CssSelector, Using = "span.dxICheckBox_Office2010Black")]
            private IWebElement checkbox;

            public Row(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Resource", "row"))
            {
            }

            public void ClickCheckbox()
            {
                GetReportUtils().ClickButton("Checkbox");
                GetWebElementUtils().clickWebElement(checkbox);
            }

        }

    }
}
