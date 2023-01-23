using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.panel
{
    public class CrewAvailabilityRequestContractorsTablePanel : BasePageObject
    {

        private static By rootLocator = By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_ASPxGridView1");

        [FindsBy(How = How.CssSelector, Using = "div.dxgvPagerBottomPanel_DevEx")]
        private IWebElement paginationRootElement;

        [FindsBy(How = How.CssSelector, Using = "tr.dxgvDataRow_DevEx")]
        private IList<IWebElement> rowRoots;

        public CrewAvailabilityRequestContractorsTablePanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Crew availability request contractors table", "panel"))
        {

        }

        public PaginationSubPanel GetPaginationSubPanel()
        {
            return new PaginationSubPanel(GetBaseInformation(), paginationRootElement);
        }

        public IList<Row> GetRows()
        {
            IList<Row> result = new List<Row>();
            foreach (IWebElement row in rowRoots)
            {
                result.Add(new Row(GetBaseInformation(), row));
            }
            return result;
        }

        public Boolean IsRowPresentByContractorOnOnePage(String contractorName)
        {
            return GetRows().Any(r => r.GetContractor().Equals(contractorName));
        }

        public Row GetRowByContractorFromOnePage(String contractorName)
        {
            return GetRows().First(r => r.GetContractor().Equals(contractorName));
        }

        public Row GetRowByContractorFromAnyPage(String contractorName)
        {
            if (IsRowPresentByContractorOnOnePage(contractorName))
            {
                return GetRowByContractorFromOnePage(contractorName);
            }
            while (GetPaginationSubPanel().IsNextButtonEnabled())
            {
                IWebElement firstRow = rowRoots[0];
                GetPaginationSubPanel().ClickNextButton();
                GetWaitUtils().waitForElementAbsent(firstRow);
                if (IsRowPresentByContractorOnOnePage(contractorName))
                {
                    return GetRowByContractorFromOnePage(contractorName);
                }
            }
            throw new AssertionException("Absent row on any page with contractor name: " + contractorName);
        }


        public class Row : BasePageObject
        {

            [FindsBy(How = How.XPath, Using = ".//td[1]/span")]
            private IWebElement selectCheckbox;

            [FindsBy(How = How.XPath, Using = ".//td[2]")]
            private IWebElement contractorLabel;

            public Row(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new WRMAutotests.Utility.ReportUtils(baseInformation, "Crew availability", "row"))
            {
            }

            public void ClickCheckbox()
            {
                GetReportUtils().AllureStep("Click Checkbox");
                GetWebElementUtils().clickWebElement(selectCheckbox);
            }

            public String GetContractor()
            {
                return contractorLabel.Text;
            }

        }




    }
}
