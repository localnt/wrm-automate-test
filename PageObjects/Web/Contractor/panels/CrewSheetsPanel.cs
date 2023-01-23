using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Contractor.pages;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.panels
{
    public class CrewSheetsPanel : BasePageObject
    {
        private static By rootLocator = By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_cpnCrewSheet_gvCrewSheet");


        [FindsBy(How = How.CssSelector, Using = "tr.dxgvDataRow_DevEx,tr.dxgvSelectedRow_DevEx")]
        private IList<IWebElement> rows;

        public CrewSheetsPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Crew Sheets", "panel"))
        {
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

        public Row GetRowByCrewSheetName(String name)
        {
            return GetRows().Where(r => r.GetCrewSheetName().Contains(name)).First();
        }

        public class Row : BasePageObject
        {

            [FindsBy(How = How.CssSelector, Using = "span.dxICheckBox_DevEx")]
            private IWebElement checkbox;

            [FindsBy(How = How.CssSelector, Using = "span.dxeBase_DevEx")]
            private IWebElement crewSheetName;

            [FindsBy(How = How.CssSelector, Using = "a[title='View the Information'] > span")]
            private IWebElement crewSheet;

            [FindsBy(How = How.CssSelector, Using = "a[title='View the Information']")]
            private IWebElement crewSheetLink;

            [FindsBy(How = How.XPath, Using = ".//td[4]")]
            private IWebElement resourceLabel;

            [FindsBy(How = How.XPath, Using = ".//td[5]")]
            private IWebElement bucketLabel;

            [FindsBy(How = How.XPath, Using = ".//td[7]")]
            private IWebElement leaderLabel;

            [FindsBy(How = How.XPath, Using = ".//td[8]")]
            private IWebElement submittedToLabel;

            public Row(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Crew Sheet", "row"))
            {
            }

            public void ClickCheckbox()
            {
                GetReportUtils().ClickButton("Checkbox");
                GetWebElementUtils().clickWebElement(checkbox);
            }

            public ResourcePoolEditCrewSheetPage ClickCrewSheetIdLink()
            {
                GetReportUtils().ClickButton("Crew Sheet Link Id");
                crewSheetLink.Click();
                return new ResourcePoolEditCrewSheetPage(GetBaseInformation());
            }

            public String GetCrewSheetName()
            {
                return crewSheetName.Text.Trim();
            }

            public String GetCrewSheet()
            {
                return crewSheet.Text.Trim();
            }

            public String GetResourceValue()
            {
                return resourceLabel.Text.Trim();
            }

            public String GetBucketValue()
            {
                return bucketLabel.Text.Trim();
            }

            public String GetLeaderValue()
            {
                return leaderLabel.Text.Trim();
            }

            public String GetSubmittedToValue()
            {
                return submittedToLabel.Text.Trim();
            }

        }

    }
}
