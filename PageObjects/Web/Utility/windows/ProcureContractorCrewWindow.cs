using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.BaseElements;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.windows
{
    public class ProcureContractorCrewWindow : BasePageObject
    {

        private static By rootLocator = By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_pbProcurement_PW-1");

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pbProcurement_cpnProcurement_chbAllProcureResources_S_D")]
        private IWebElement allResourceCheckbox;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pbProcurement_cpnProcurement_chbAllProcureBuckets_S_D")]
        private IWebElement allBucketsCheckbox;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pbProcurement_cpnProcurement_chbAllProcureDiggers_S_D")]
        private IWebElement allDiggerCheckbox;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pbProcurement_UtilityLocations_cpnUSCUtilityLocation_cbUSCUtilityLocationDestination_ET")]
        private IWebElement destinationDropdownMenu;

        [FindsBy(How = How.CssSelector, Using = "div.popupProcurementWorkStatusRow .dxichTextCellSys label")]
        private IList<IWebElement> statusOfWork;

        [FindsBy(How = How.CssSelector, Using = ".Main-ScrollBarAutoForToken")]
        private IWebElement assignedSupervisorRoot;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pbProcurement_txtAdditionalRequirement_I")]
        private IWebElement additionalRequirement;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pbProcurement_btnSubmitProcurement")]
        private IWebElement procureCrewsButton;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pbProcurement_dtOncallStart_B-1")]
        private IWebElement onCallWorkStatusStartingDateDropdownMenu;

        [FindsBy(How = How.CssSelector, Using = "div#ASPxPanel2_ContentPlaceHolder1_pbProcurement_dtOncallStart_DDD_PW-1")]
        private IWebElement onCallWorkStatusStartingDateRootPanel;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pbProcurement_dtOncallEnd_B-1")]
        private IWebElement onCallWorkStatusEndingDateDropdownMenu;

        [FindsBy(How = How.CssSelector, Using = "div#ASPxPanel2_ContentPlaceHolder1_pbProcurement_dtOncallEnd_DDD_PW-1")]
        private IWebElement onCallWorkStatusEndingDateRootPanel;

        [FindsBy(How = How.CssSelector, Using = "td#ASPxPanel2_ContentPlaceHolder1_pbProcurement_dtStandbyStart_B-1")]
        private IWebElement standByWorkStatusStartingDateDropdownMenu;

        [FindsBy(How = How.CssSelector, Using = "div#ASPxPanel2_ContentPlaceHolder1_pbProcurement_dtStandbyStart_DDD_PW-1")]
        private IWebElement standByWorkStatusStartingDateRootPanel;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pbProcurement_dtStandbyEnd_B-1")]
        private IWebElement standByWorkStatusEndingDateDropdownMenu;

        [FindsBy(How = How.CssSelector, Using = "div#ASPxPanel2_ContentPlaceHolder1_pbProcurement_dtStandbyEnd_DDD_PW-1")]
        private IWebElement standByWorkStatusEndingDateRootPanel;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pbProcurement_dtMobilizeOnStart_B-1")]
        private IWebElement mobilizaOnWorkStatusDateDropdownMenu;

        [FindsBy(How = How.CssSelector, Using = "div#ASPxPanel2_ContentPlaceHolder1_pbProcurement_dtMobilizeOnStart_DDD_PW-1")]
        private IWebElement mobilizaOnWorkStatusDatRootPanel;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pbProcurement_dtMobilizePlantoArrivebyStart_B-1")]
        private IWebElement planToArriveByWorkStatusDateDropdownMenu;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_pbProcurement_dtMobilizePlantoArrivebyStart_DDD_PW-1")]
        private IWebElement planToArriveByWorkStatusDatRootPanel;

        [FindsBy(How = How.XPath, Using = ".//td[contains(@class, 'dxgvBatchEditModifiedCell_DevEx dxgv')][1]")]
        private IWebElement resourceProcureCell;

        [FindsBy(How = How.XPath, Using = ".//td[contains(@class, 'dxgvBatchEditModifiedCell_DevEx dxgv')][2]")]
        private IWebElement bucketProcureCell;

        [FindsBy(How = How.XPath, Using = ".//td[contains(@class, 'dxgvBatchEditModifiedCell_DevEx dxgv')][3]")]
        private IWebElement diggerProcureCell;

        [FindsBy(How = How.CssSelector, Using = "table#ASPxPanel2_ContentPlaceHolder1_pbProcurement_UtilityLocations_rdnUSCUtilityLocation .dxeBase_DevEx.dxeTAR")]
        private IList<IWebElement> destinations;

        public ProcureContractorCrewWindow(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Procure Contactor Crew", "window"))
        {
        }

        public void ClickCityStateDestination()
        {
            GetReportUtils().ClickButton("City/State destination");
            destinations[0].Click();
            GetWaitUtils().WaitForLoadingPanelAbsent();
        }

        public void ClickSiteDestination()
        {
            GetReportUtils().ClickButton("Site destination");
            destinations[1].Click();
            GetWaitUtils().WaitForLoadingPanelAbsent();
        }

        public void ClickHotelDestination()
        {
            GetReportUtils().ClickButton("Hotel destination");
            destinations[2].Click();
            GetWaitUtils().WaitForLoadingPanelAbsent();
        }

        public void ClickRegionDestination()
        {
            GetReportUtils().ClickButton("Region destination");
            destinations[3].Click();
            GetWaitUtils().WaitForLoadingPanelAbsent();
        }

        public void EnterResourceProcure(String resource)
        {
            GetReportUtils().EnterValueToField("Resource Procure", resource);
            resourceProcureCell.Click();
            Thread.Sleep(1000);
            GetWebElementUtils().enterValueToFieldWithoutWaitEntering(resourceProcureCell.FindElement(By.CssSelector("input")), resource);
        }

        public void EnterBucketProcure(String bucket)
        {
            GetReportUtils().EnterValueToField("Resource Procure", bucket);
            bucketProcureCell.Click();
            Thread.Sleep(1000);
            GetWebElementUtils().enterValueToFieldWithoutWaitEntering(bucketProcureCell.FindElement(By.CssSelector("input")), bucket);
        }

        public void EnterDiggerProcure(String digger)
        {
            GetReportUtils().EnterValueToField("Resource Procure", digger);
            diggerProcureCell.Click();
            Thread.Sleep(1000);
            GetWebElementUtils().enterValueToFieldWithoutWaitEntering(diggerProcureCell.FindElement(By.XPath(".//input")), digger);
        }

        public void SelectDateForPlanToArriveByWorkStatus(DateTime dateTime)
        {
            GetReportUtils().AllureStepWithPageObject("Select Date time for Plan to Arrive By Work status");
            GetWebElementUtils().clickWebElement(planToArriveByWorkStatusDateDropdownMenu);
            Thread.Sleep(3000);
            new DateTimeSelectorWindow(GetBaseInformation(), planToArriveByWorkStatusDatRootPanel).SelectDate(dateTime);
        }

        public void SelectDateForMobilizeOnWorkStatus(DateTime dateTime)
        {
            GetReportUtils().AllureStepWithPageObject("Select Date time for Mobilize On Work status");
            GetWebElementUtils().clickWebElement(mobilizaOnWorkStatusDateDropdownMenu);
            Thread.Sleep(3000);
            new DateTimeSelectorWindow(GetBaseInformation(), mobilizaOnWorkStatusDatRootPanel).SelectDate(dateTime);
        }

        public void SelectStartingDateForStandByWorkStatus(DateTime dateTime)
        {
            GetReportUtils().AllureStepWithPageObject("Select Starting Time for StandBy Work status");
            GetWebElementUtils().clickWebElement(standByWorkStatusStartingDateDropdownMenu);
            Thread.Sleep(3000);
            new DateTimeSelectorWindow(GetBaseInformation(), standByWorkStatusStartingDateRootPanel).SelectDate(dateTime);
        }

        public void SelectEndingDateForStandByWorkStatus(DateTime dateTime)
        {
            GetReportUtils().AllureStepWithPageObject("Select Ending Time for StandBy Work status");
            GetWebElementUtils().clickWebElement(standByWorkStatusEndingDateDropdownMenu);
            Thread.Sleep(3000);
            new DateTimeSelectorWindow(GetBaseInformation(), standByWorkStatusEndingDateRootPanel).SelectDate(dateTime);
        }


        public void SelectStartingDateForOnCallWorkStatus(DateTime dateTime)
        {
            GetReportUtils().AllureStepWithPageObject("Select Starting Time for On Call Work status");
            GetWebElementUtils().clickWebElement(onCallWorkStatusStartingDateDropdownMenu);
            Thread.Sleep(3000);
            new DateTimeSelectorWindow(GetBaseInformation(), onCallWorkStatusStartingDateRootPanel).SelectDate(dateTime);
        }

        public void SelectEndingDateForOnCallWorkStatus(DateTime dateTime)
        {
            GetReportUtils().AllureStepWithPageObject("Select Ending Time for On Call Work status");
            GetWebElementUtils().clickWebElement(onCallWorkStatusEndingDateDropdownMenu);
            Thread.Sleep(3000);
            new DateTimeSelectorWindow(GetBaseInformation(), onCallWorkStatusEndingDateRootPanel).SelectDate(dateTime);
        }

        public void ClickAllResourcesCheckbox()
        {
            GetReportUtils().ClickButton("Add All Resource checkbox");
            GetWebElementUtils().clickWebElement(allResourceCheckbox);
        }

        public void ClickAllBucketCheckbox()
        {
            GetReportUtils().ClickButton("Add All Resource checkbox");
            GetWebElementUtils().clickWebElement(allBucketsCheckbox);
        }

        public void ClickAllDiggersCheckbox()
        {
            GetReportUtils().ClickButton("Add All Resource checkbox");
            GetWebElementUtils().clickWebElement(allDiggerCheckbox);
        }

        public void SelectDestination(String destination)
        {
            (new StandardDropdownMenu(GetBaseInformation(), destinationDropdownMenu, new ReportUtils(GetBaseInformation(), "Destination", "dropdown menu"))).SelectMenuElement(destination);
        }

        public void SelectWorkStatus(WorkStatus workStatus)
        {
            String workStatusText = "";
            switch (workStatus)
            {
                case (WorkStatus.On_Call):
                    {
                        workStatusText = "On Call";
                        break;
                    }
                case (WorkStatus.Standby):
                    {
                        workStatusText = "Standby";
                        break;
                    }
                case (WorkStatus.Immediatly_Mobilize):
                    {
                        workStatusText = "Immediately Mobilize";
                        break;
                    }
                case (WorkStatus.Mobilize_On):
                    {
                        workStatusText = "Mobilize on";
                        break;
                    }
                case (WorkStatus.Plan_To_Arrive_By):
                    {
                        workStatusText = "Plan to Arrive by";
                        break;
                    }
            }

            GetReportUtils().AllureStepWithPageObject("Select Work status: " + workStatusText);
            IWebElement targetStatus = statusOfWork.Where(r => r.Text.Equals(workStatusText)).First();
            GetWebElementUtils().clickWebElement(targetStatus);
        }

        public void AddAssignedSupervisor(String assignedSupervisor)
        {
            GetReportUtils().AllureStepWithPageObject("Select Assigned Supervisor: " + assignedSupervisor);
            IWebElement field = assignedSupervisorRoot.FindElement(By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_pbProcurement_tkbSupervisor_I"));
            GetWebElementUtils().enterValueToFieldWithWaitEntering(field, assignedSupervisor);
            Thread.Sleep(5000);
            IList<IWebElement> possibleUsers = assignedSupervisorRoot.FindElements(By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_pbProcurement_tkbSupervisor_DDD_L_LBT tr.dxeListBoxItemRow_DevEx"))
                .Where(r => r.FindElement(By.CssSelector(".dxeListBoxItem_DevEx")).Text.Equals(assignedSupervisor))
                .ToList();
            GetWebElementUtils().clickWebElement(possibleUsers[0]);
            Thread.Sleep(3000);
        }

        public void EnterAdditionalRequirement(String requiment)
        {
            GetReportUtils().EnterValueToField("Additional Requirement", requiment);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(additionalRequirement, requiment);
        }

        public void ClickProcureCrewsButton()
        {
            GetReportUtils().ClickButton("Procure Crews");
            GetWebElementUtils().clickWebElement(procureCrewsButton);
            GetWaitUtils().WaitForElementInvisible(By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_ASPxLoadingPanel1"));
            Thread.Sleep(2000);
        }

        public enum WorkStatus
        {
            On_Call,
            Standby,
            Immediatly_Mobilize,
            Mobilize_On,
            Plan_To_Arrive_By
        }

        public static WorkStatus GetWorkStatusByNameOfWorkStatus(String workStatusName)
        {
            if (workStatusName.Equals("On Call"))
                return WorkStatus.On_Call;
            if (workStatusName.Equals("Standby"))
                return WorkStatus.Standby;
            if (workStatusName.Equals("Immediately Mobilize"))
                return WorkStatus.Immediatly_Mobilize;
            if (workStatusName.Equals("Mobilize on"))
                return WorkStatus.Mobilize_On;
            if (workStatusName.Equals("Plan to Arrive by"))
                return WorkStatus.Plan_To_Arrive_By;
            throw new AssertionException("Absent working status enum for text: " + workStatusName);
        }


    }
}
