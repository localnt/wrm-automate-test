using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.panel
{
    public class EventsPanel : BasePageObject
    {
        private static By rootLocator = By.CssSelector("div[id*='ContentPlaceHolder'] > table.dxgvControl_DevEx");

        [FindsBy(How = How.CssSelector, Using = "tr.dxgvDataRow_DevEx,.dxgvSelectedRow_DevEx")]
        private IList<IWebElement> rowRootEvelnts;

        [FindsBy(How = How.CssSelector, Using = "div[class*='PagerBottomPanel']")]
        private IWebElement paginationRoot;

        public EventsPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Events", "panel"))
        {
            GetWaitUtils().WaitForAllListElementsPresent(rowRootEvelnts);
            Thread.Sleep(10000);
        }

        public WRMAutotests.PageObjects.Web.Utility.panel.EventsPanel GetEventsPanel()
        {
            return new WRMAutotests.PageObjects.Web.Utility.panel.EventsPanel(GetBaseInformation());
        }

        public PaginationSubPanel GetPaginationSubPanel()
        {
            return new PaginationSubPanel(GetBaseInformation(), paginationRoot);
        }

        public IList<Row> GetRows()
        {
            IList<Row> rows = new List<Row>();
            foreach (IWebElement row in rowRootEvelnts)
            {
                rows.Add(new Row(GetBaseInformation(), row));
            }
            return rows;
        }

        public Row GetRowByCodeFromOnePage(String code)
        {
            return GetRows().First(r => r.GetCode().Equals(code));
        }

        public Boolean IsRowWithCodePresentOnAnyPage(String code)
        {
            if (IsRowPresentOnCurrentPage(code))
                return true;
            while (GetPaginationSubPanel().IsNextButtonEnabled())
            {
                if (IsRowPresentOnCurrentPage(code))
                    return true;
                GetPaginationSubPanel().ClickNextButton();
            }
            return false;
        }

        public Row GetRowByCodeFromAnyPage(String code)
        {
            if (IsRowPresentOnCurrentPage(code))
            {
                return GetRowByCodeFromOnePage(code);
            }
            while (GetPaginationSubPanel().IsNextButtonEnabled())
            {
                if (IsRowPresentOnCurrentPage(code))
                {
                    return GetRowByCodeFromOnePage(code);
                }
                GetPaginationSubPanel().ClickNextButton();
            }
            throw new AssertionException("Absent event with code: " + code);
        }

        public Boolean IsRowPresentOnCurrentPage(String code)
        {
            foreach (Row row in GetRows())
            {
                if (row.GetCode().Equals(code))
                    return true;
            }
            return false;
        }


        public class Row : BasePageObject
        {
            [FindsBy(How = How.XPath, Using = ".//td[@class='dxgv'][1]")]
            private IWebElement operation;

            [FindsBy(How = How.CssSelector, Using = "td > a[title='View the Information']")]
            private IWebElement code;

            [FindsBy(How = How.XPath, Using = ".//td[@class='dxgv'][3]")]
            private IWebElement name;

            [FindsBy(How = How.XPath, Using = ".//td[@class='dxgv'][4]")]
            private IWebElement type;

            [FindsBy(How = How.XPath, Using = ".//td[@class='dxgv'][5]")]
            private IWebElement description;

            [FindsBy(How = How.XPath, Using = ".//td[@class='dxgv'][6]/span")]
            private IWebElement status;

            [FindsBy(How = How.XPath, Using = ".//td[@class='dxgv'][7]/span")]
            private IWebElement defaultStatus;

            [FindsBy(How = How.XPath, Using = ".//td[@class='dxgv'][8]")]
            private IWebElement updated;

            [FindsBy(How = How.XPath, Using = ".//td[@class='dxgv'][9]")]
            private IWebElement updatedBy;

            [FindsBy(How = How.XPath, Using = ".//td[@class='dxgv'][10]")]
            private IWebElement operatingCompany;

            public Row(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new WRMAutotests.Utility.ReportUtils(baseInformation, "Event", "row"))
            {
            }

            public String GetCode()
            {
                return code.Text
                    .Trim();
            }

            public String GetName()
            {
                return name.Text
                    .Trim();
            }

            public String GetType()
            {
                return type.Text
                    .Trim();
            }

            public String GetDescription()
            {
                return description.Text
                    .Trim();
            }

            public String GetStatus()
            {
                return status.Text
                    .Trim();
            }

            public Boolean IsDefault()
            {
                return defaultStatus.GetAttribute("class")
                    .Contains("edtCheckBoxChecked_DevEx");
            }

            public String GetUpdated()
            {
                return updated.Text
                    .Trim();
            }

            public String GetUpdatedBy()
            {
                return updatedBy.Text
                    .Trim();
            }

            public String GetOperatingCompany()
            {
                return operatingCompany.Text
                    .Trim();
            }

            public EventPage ClickOperationButton()
            {
                GetReportUtils().ClickButton("Operation");
                GetWebElementUtils().clickWebElement(operation);
                return new EventPage(GetBaseInformation());
            }


        }


    }
}
