using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Utility.windows;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.panel
{
    public class SecuredWorkforcesPanel : BasePageObject
    {
        private static By rootLocator = By.CssSelector("table#ASPxPanel2_ContentPlaceHolder1_cpnTokenDeleted_cpnMain_cpnRefreshGridViewAndSummary_ASPxGridView1");

        [FindsBy(How = How.CssSelector, Using = "tr.dxgvDataRow_DevEx,tr.dxgvSelectedRow_DevEx")]
        private IList<IWebElement> rows;

        [FindsBy(How = How.CssSelector, Using = "div.dxgvPagerBottomPanel_DevEx")]
        private IWebElement paginationBarRootElement;

        public SecuredWorkforcesPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Secured Workforce", "panel"))
        {
        }

        public IList<Row> GetRows()
        {
            IList<Row> result = new List<Row>();
            foreach (IWebElement element in rows)
            {
                GetWebElementUtils().ScrollToElement(element);
                result.Add(new Row(GetBaseInformation(), element));
            }
            return result;
        }

        public IList<Row> GetRowsByResourcePool(String resourcePool)
        {
            return GetRows().Where(row => row.GetResourcePool().Equals(resourcePool)
            ).ToList();
        }

        public Row GetRowByResourcePoolFromAmyPage(String resourcePool)
        {
            GetPaginationSubPanel().ClickFirstPage();
            if (GetRowsByResourcePool(resourcePool).Count > 0)
            {
                return GetRowsByResourcePool(resourcePool)[0];
            }
            while (GetPaginationSubPanel().IsNextButtonEnabled())
            {
                GetPaginationSubPanel().ClickNextButton();
                if (GetRowsByResourcePool(resourcePool).Count > 0)
                {
                    return GetRowsByResourcePool(resourcePool)[0];
                }
            }
            throw new AssertionException("Absent row with resource pool: " + resourcePool);
        }

        public int GetNumberOFRowsByResourcePoolFromAllPages(String resourcePool)
        {
            GetPaginationSubPanel().ClickFirstPage();
            int result = 0;
            result += GetRowsByResourcePool(resourcePool).Count;
            while (GetPaginationSubPanel().IsNextButtonEnabled())
            {
                GetPaginationSubPanel().ClickNextButton();
                result += GetRowsByResourcePool(resourcePool).Count;
            }
            return result;

        }

        public PaginationSubPanel GetPaginationSubPanel()
        {
            return new PaginationSubPanel(GetBaseInformation(), paginationBarRootElement);
        }


        public class Row : BasePageObject
        {

            [FindsBy(How = How.XPath, Using = "./td[1]/span")]
            private IWebElement checkbox;

            [FindsBy(How = How.XPath, Using = "./td[3]")]
            private IWebElement resourcePool;

            [FindsBy(How = How.XPath, Using = "./td[20]")]
            private IWebElement supervisor;

            [FindsBy(How = How.XPath, Using = "./td[21]")]
            private IWebElement assignedLocation;

            [FindsBy(How = How.XPath, Using = "./td[22]")]
            private IWebElement eta;

            [FindsBy(How = How.XPath, Using = "./td[23]/span/span")]
            private IWebElement etaComment;

            [FindsBy(How = How.XPath, Using = "./td[29]")]
            private IWebElement eventName;

            [FindsBy(How = How.CssSelector, Using = "a.dxbButton_DevEx.dxbButtonSys")]
            private IWebElement crewSheetLink;


            public Row(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Secured Workforce", "row"))
            {
            }

            public String GetEventName()
            {
                GetWebElementUtils().ScrollToElement(eventName);
                return eventName.GetAttribute("innerHTML").Trim();
            }

            public String GetEtaComment()
            {
                GetWebElementUtils().ScrollToElement(etaComment);
                return etaComment.GetAttribute("innerHTML").Trim();
            }

            public String GetEta()
            {
                GetWebElementUtils().ScrollToElement(eta);
                return eta.GetAttribute("innerHTML").Trim();
            }

            public String GetAssignedLocation()
            {
                GetWebElementUtils().ScrollToElement(assignedLocation);
                return assignedLocation.GetAttribute("innerHTML").Trim();
            }

            public String GetSupervisor()
            {
                GetWebElementUtils().ScrollToElement(supervisor);
                return supervisor.GetAttribute("innerHTML").Trim();
            }

            public void ClickChecbox()
            {
                GetReportUtils().ClickButton("Checkbox");
                Thread.Sleep(1000);
                checkbox.Click();
                Thread.Sleep(1000);
            }

            public String GetResourcePool()
            {
                return resourcePool.Text;
            }

            public CrewSheetOrganizationWindow ClickCrewSheetLink()
            {
                GetReportUtils().ClickButton("Crew Sheet");
                crewSheetLink.Click();
                GetWaitUtils().WaitForLoadingPanelAbsent();
                return new CrewSheetOrganizationWindow(GetBaseInformation());
            }

            public String getCrewSheet()
            {
                return crewSheetLink.FindElement(By.CssSelector("span"))
                    .Text
                    .Trim();
            }

        }

    }
}
