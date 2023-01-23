using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.panel
{
    public class CrewAvailabilitiesPanel : BasePageObject
    {
        private static By rootLocator = By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_ASPxGridView1");

        [FindsBy(How = How.CssSelector, Using = "tr.dxgvDataRow_DevEx")]
        private IList<IWebElement> rows;

        [FindsBy(How = How.CssSelector, Using = "div.dxgvPagerBottomPanel_DevEx")]
        private IWebElement paginationRootElement;

        public CrewAvailabilitiesPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Crew Availabilities", "panel"))
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
            return GetRows().Where(row => row.GetResourcePool().Equals(resourcePool))
                .ToList();
        }

        public IList<Row> GetRowsByResourcePoolFromAnyPage(String resourcePool)
        {
            GetPagination().ClickFirstPage();
            if (GetRowsByResourcePool(resourcePool).Count > 0)
            {
                return GetRowsByResourcePool(resourcePool);
            }
            while (GetPagination().IsNextButtonEnabled())
            {
                GetPagination().ClickNextButton();
                if (GetRowsByResourcePool(resourcePool).Count > 0)
                {
                    return GetRowsByResourcePool(resourcePool);
                }
            }
            throw new AssertionException("Resource pool absent. Expected resource pool: " + resourcePool);
        }

        public IList<Row> GetRowsByResourcePoolName(String resourcePoolName)
        {
            return GetRows().Where(row => row.GetResourcePoolName().Equals(resourcePoolName))
                .ToList();
        }

        public Boolean IsRowPresentOnAnyPageByResourcePool(String resourcePool)
        {
            if (GetRowsByResourcePool(resourcePool).Count > 0)
                return true;
            while (GetPagination().IsNextButtonEnabled())
            {
                GetPagination().ClickNextButton();
                if (GetRowsByResourcePool(resourcePool).Count > 0)
                    return true;
            }
            return false;
        }


        public Boolean IsRowPresentOnAnyPageByResourcePoolName(String resourcePoolName)
        {
            if (GetRowsByResourcePoolName(resourcePoolName).Count > 0)
                return true;
            while (GetPagination().IsNextButtonEnabled())
            {
                GetPagination().ClickNextButton();
                if (GetRowsByResourcePoolName(resourcePoolName).Count > 0)
                    return true;
            }
            return false;
        }

        public Row GetRowFromAnyPageByResourcePoolName(String resourcePoolName)
        {
            if (GetRowsByResourcePoolName(resourcePoolName).Count > 0)
                return GetRowsByResourcePoolName(resourcePoolName).First();
            while (GetPagination().IsNextButtonEnabled())
            {
                GetPagination().ClickNextButton();
                if (GetRowsByResourcePoolName(resourcePoolName).Count > 0)
                    return GetRowsByResourcePoolName(resourcePoolName).First();
            }
            throw new AssertionException("Absent row with Resource pool name: " + resourcePoolName);
        }

        private PaginationSubPanel GetPagination()
        {
            return new PaginationSubPanel(GetBaseInformation(), paginationRootElement);
        }


        public class Row : BasePageObject
        {

            [FindsBy(How = How.XPath, Using = "./td[3]")]
            private IWebElement resourcePoolLabel;

            [FindsBy(How = How.CssSelector, Using = "span.dxICheckBox_DevEx ")]
            private IWebElement selectionCheckbox;

            [FindsBy(How = How.XPath, Using = "./td[4]")]
            private IWebElement resourcePoolNameLabel;

            [FindsBy(How = How.CssSelector, Using = "td img[title='Procurement Request Accepted']")]
            private IWebElement requestAcceptedLabel;

            public Row(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Crew Availability", "row"))
            {
            }

            public void ClickCheckbox()
            {
                GetReportUtils().ClickButton("Selection checbox");
                GetWebElementUtils().clickWebElement(selectionCheckbox);
                Thread.Sleep(3000);
            }

            public String GetResourcePoolName()
            {
                return resourcePoolNameLabel.Text;
            }

            public String GetResourcePool()
            {
                return resourcePoolLabel.Text;
            }

            public Boolean IsRequestAcceptedLabelPresent()
            {
                return GetWebElementUtils().IsWebElementPresent(requestAcceptedLabel);
            }


        }


    }
}
