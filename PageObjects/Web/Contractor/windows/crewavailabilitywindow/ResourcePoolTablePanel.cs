using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.windows.crewavailabilitywindow
{
    public class ResourcePoolTablePanel : BasePageObject
    {

        [FindsBy(How = How.CssSelector, Using = "tr.dxgvDataRow_DevEx")]
        private IList<IWebElement> rows;

        public ResourcePoolTablePanel(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Resource Pool table on Crew availability form window", "panel"))
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

        public IList<Row> GetRowsByResourcePool(String resourcePool)
        {
            return GetRows().Where(r => r.GetResourcePool().Equals(resourcePool)).ToList();
        }

        public class Row : BasePageObject
        {

            [FindsBy(How = How.CssSelector, Using = "span.dxICheckBox_DevEx")]
            private IWebElement selectorCheckbox;

            [FindsBy(How = How.CssSelector, Using = "a[title='View the Information']")]
            private IWebElement resorcePool;

            public Row(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Resource Pool on Crew Availability Form", "row"))
            {
            }

            public void ClickCheckbox()
            {
                GetReportUtils().ClickButton("Checkbox");
                GetWebElementUtils().clickWebElement(selectorCheckbox);
                Thread.Sleep(3000);
            }

            public String GetResourcePool()
            {
                return resorcePool.Text;
            }

        }


    }
}
