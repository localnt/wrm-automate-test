using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.windows.crewavailabilitywindow
{
    public class UtilitiesTablePanel : BasePageObject
    {

        [FindsBy(How = How.CssSelector, Using = "tr.dxgvDataRow_DevEx")]
        private IList<IWebElement> rows;

        public UtilitiesTablePanel(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Utilities table", "panel"))
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

        public IList<Row> GetRowsByUtilityName(String name)
        {
            return GetRows().Where(r => r.GetUtilityName().Equals(name)).ToList();
        }

        public class Row : BasePageObject
        {

            [FindsBy(How = How.CssSelector, Using = "span.dxICheckBox_DevEx")]
            private IWebElement selectorCheckbox;

            [FindsBy(How = How.XPath, Using = "./td[3]")]
            private IWebElement utilityName;

            public Row(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Resource Pool on Crew Availability Form", "row"))
            {
            }

            public void ClickCheckbox()
            {
                GetReportUtils().ClickButton("Checkbox");
                GetWebElementUtils().clickWebElement(selectorCheckbox);
                Thread.Sleep(2000);
            }

            public String GetUtilityName()
            {
                return utilityName.Text;
            }

        }


    }
}
