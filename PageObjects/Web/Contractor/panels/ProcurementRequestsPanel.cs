using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Contractor.pages;
using WRMAutotests.PageObjects.Web.Utility.panel;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.panels
{
    public class ProcurementRequestsPanel : BasePageObject
    {

        private static By rootLocator = By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_UpdatePanel3");

        [FindsBy(How = How.CssSelector, Using = "tr.dxgvDataRow_DevEx")]
        private IList<IWebElement> rows;

        [FindsBy(How = How.CssSelector, Using = "div.dxgvPagerBottomPanel_DevEx")]
        private IWebElement paginationRootElement;


        public ProcurementRequestsPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Procurement Requests", "panel"))
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

        public IList<Row> GetRowsFromCurrentPageByResourcePool(String resourcePool)
        {
            return GetRows().Where(r => r.GetResourcePool().Equals(resourcePool))
                .ToList();
        }

        public Boolean IsRowPresentOnCurrentPageByResourcePool(String resourcePool)
        {
            return GetRowsFromCurrentPageByResourcePool(resourcePool).Count > 0;
        }

        public Boolean IsRowPresentOnAnyPageByResourcePool(String resourcePool)
        {
            if (IsRowPresentOnCurrentPageByResourcePool(resourcePool))
                return true;
            else
            {
                while (GetPaginationSubPanel().IsNextButtonEnabled())
                {
                    GetPaginationSubPanel().ClickNextButton();
                    if (IsRowPresentOnCurrentPageByResourcePool(resourcePool))
                        return true;
                }
                return false;
            }
        }

        public PaginationSubPanel GetPaginationSubPanel()
        {
            return new PaginationSubPanel(GetBaseInformation(), paginationRootElement);
        }


        public class Row : BasePageObject
        {

            [FindsBy(How = How.CssSelector, Using = "div[title='Edit the record']")]
            private IWebElement editButton;

            [FindsBy(How = How.XPath, Using = "./td[4]")]
            private IWebElement status;

            [FindsBy(How = How.XPath, Using = "./td[5]")]
            private IWebElement resourcePool;

            public Row(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Procurement Request", "row"))
            {
            }

            public ProcurementResponsePage ClickEditButton()
            {
                GetReportUtils().ClickButton("Edit");
                GetWebElementUtils().clickWebElement(editButton);
                return new ProcurementResponsePage(GetBaseInformation());
            }

            public String GetResourcePool()
            {
                return resourcePool.Text;
            }

            public String GetStatus()
            {
                return status.Text;
            }

        }


    }
}
