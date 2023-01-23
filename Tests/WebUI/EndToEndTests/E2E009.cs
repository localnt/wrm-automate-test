using MimeKit;
using Newtonsoft.Json;
using RetryOnException;
using WRMAutotests.PageObjects.Web.Contractor.pages;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.PageObjects.Web.Utility.windows;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.Tests.WebUI.EndToEndTests
{
    public class E2E009 : BaseEndToEndTest
    {

        private static String operatingCompanyName = excelReadedUtils.GetCellValue(1, 11, 2).Equals("") ? defaultOperatingCompany : excelReadedUtils.GetCellValue(1, 11, 2);
        private static String eventName = excelReadedUtils.GetCellValue(1, 11, 3).Equals("") ? defaultEvent : excelReadedUtils.GetCellValue(1, 11, 3);
        private static String discipline = excelReadedUtils.GetCellValue(1, 11, 4).Equals("") ? defaultDiscipline : excelReadedUtils.GetCellValue(1, 11, 4);

        private static InputFormSettings settingsInputForm = JsonConvert.DeserializeObject<InputFormSettings>(excelReadedUtils.GetCellValue(1, 11, 5));

        private String resourcePoolName = GetRandomValuesUtilities().GetRandomValue();
        private InternalWorkforcePage.Tabs tabDiscipline = InternalWorkforcePage.GetTabByNameFoTab(discipline);
        private String sourceLocation = settingsInputForm.Location;

        [SetUp]
        public void CreateNeedEnteties()
        {
            int estimatedResources = 5;
            int estimatedCrews = 3;
            int crewSize = 4;
            int estimatedBuckets = 6;
            int estimatedDiggers = 6;

            BaseInformation baseInformation = AddNewDriverWithDefaultSettings();
            MainPage mainPage = LoginUntoDefaultContractor(baseInformation, contractorUser);
            InternalWorkforcePage internalWorkforcePage = CreateResourcePool(mainPage, tabDiscipline, resourcePoolName, sourceLocation, estimatedResources, estimatedCrews, crewSize, estimatedBuckets, estimatedDiggers, checkIouCheckbox, operatingCompanyName);
            internalWorkforcePage = CreateCrewAvailabilityForm(internalWorkforcePage, resourcePoolName, operatingCompanyName);
            RemoveDriver(baseInformation);


        }

        [RetryOnException(ListOfExceptions = new[] { typeof(Exception) })]
        [Retry(numberOfTryFroWebTests)]
        [Test]
        public void E2E009_Test()
        {
            DefaultPage defaultPage = OpenDefaultUtilityPage();
            WRMAutotests.PageObjects.Web.Utility.pages.LoginPage loginPage = defaultPage.ClickLoginButton();
            loginPage.GetLoginPanel().EnterEmail(utilityUser.GetEmail());
            loginPage.GetLoginPanel().EnterPassword((utilityUser.GetPassword()));
            EventSelectionPage eventSelectionPage = loginPage.GetLoginPanel().ClickLoginButton();
            ManageSecuredWorkforcePage manageSecuredWorkforcePage = eventSelectionPage.GetWelcomeToStormManagerPanel().ClickAccessWithoutEventSelection();
            NonIouMarketplacePage nonIouMarketplacePage = manageSecuredWorkforcePage.GetHeaderPanel()
                .OpenWorkforceEventsManuPanel()
                .ClickNonIouMarketplaceButton();
            nonIouMarketplacePage.GetHeaderPanel()
                .OpenOperatingCompanyDropdownMenu()
                .ClickOperatingCompanyByName(operatingCompanyName);
            nonIouMarketplacePage.GetHeaderPanel()
                .OpenEventDropdownMenu()
                .ClickEventByName(eventName);
            nonIouMarketplacePage.GetLocationDropDownMenu()
                .SelectMenuElement(sourceLocation);
            nonIouMarketplacePage.ClickUpdateButton();
            WRMAutotests.PageObjects.Web.Utility.panel.CrewAvailabilitiesPanel.Row targetrow = nonIouMarketplacePage.GetCrewAvailabilitiesPanel()
                .GetRowFromAnyPageByResourcePoolName(resourcePoolName);
            String resourcePool = targetrow.GetResourcePool();
            targetrow.ClickCheckbox();
            ProcureContractorCrewWindow procureContractorCrewWindow = nonIouMarketplacePage.ClickProcureCrew();

            //Chouse expected resources
            //we can chouse from excel, we want to click add all or add value manualy
            if (settingsInputForm.ResourceAll)
            {
                procureContractorCrewWindow.ClickAllResourcesCheckbox();
            }
            else
            {
                procureContractorCrewWindow.EnterResourceProcure(settingsInputForm.ResourceAmount);
            }
            if (settingsInputForm.BucketAll)
            {
                procureContractorCrewWindow.ClickAllBucketCheckbox();
            }
            else
            {
                procureContractorCrewWindow.EnterBucketProcure(settingsInputForm.BucketAmount);
            }
            if (settingsInputForm.DiggerAll)
            {
                procureContractorCrewWindow.ClickAllDiggersCheckbox();
            }
            else
            {
                procureContractorCrewWindow.EnterDiggerProcure(settingsInputForm.DiggerAmount);
            }

            //Chouse destination
            if (settingsInputForm.Destination.ToLower().Contains("city"))
            {
                procureContractorCrewWindow.ClickCityStateDestination();
            }
            else if (settingsInputForm.Destination.ToLower().Contains("site"))
            {
                procureContractorCrewWindow.ClickSiteDestination();
            }
            else if (settingsInputForm.Destination.ToLower().Contains("hotel"))
            {
                procureContractorCrewWindow.ClickHotelDestination();
            }
            else if (settingsInputForm.Destination.ToLower().Contains("region"))
            {
                procureContractorCrewWindow.ClickRegionDestination();
            }
            else
            {
                throw new AssertionException("Unexpected destination from settings: " + settingsInputForm.Destination);
            }
            procureContractorCrewWindow.SelectDestination(settingsInputForm.Location);

            //Chouse work status an Assignment
            ProcureContractorCrewWindow.WorkStatus targetWorkStatus = ProcureContractorCrewWindow.GetWorkStatusByNameOfWorkStatus(settingsInputForm.WorkStatus);
            procureContractorCrewWindow.SelectWorkStatus(targetWorkStatus);
            DateTime startingDateTime = DateTime.Parse(settingsInputForm.StartDate);
            DateTime endingDateTime = DateTime.Parse(settingsInputForm.EndDate);
            if (targetWorkStatus.Equals(ProcureContractorCrewWindow.WorkStatus.On_Call))
            {
                procureContractorCrewWindow.SelectStartingDateForOnCallWorkStatus(startingDateTime);
                procureContractorCrewWindow.SelectEndingDateForOnCallWorkStatus(endingDateTime);
            }
            else if (targetWorkStatus.Equals(ProcureContractorCrewWindow.WorkStatus.Standby))
            {
                procureContractorCrewWindow.SelectStartingDateForStandByWorkStatus(startingDateTime);
                procureContractorCrewWindow.SelectEndingDateForStandByWorkStatus(endingDateTime);
            }
            else if (targetWorkStatus.Equals(ProcureContractorCrewWindow.WorkStatus.Immediatly_Mobilize))
            {
                //Here absent date fields
            }
            else if (targetWorkStatus.Equals(ProcureContractorCrewWindow.WorkStatus.Mobilize_On))
            {
                //Here present only one Date
                procureContractorCrewWindow.SelectDateForMobilizeOnWorkStatus(startingDateTime);
            }
            else if (targetWorkStatus.Equals(ProcureContractorCrewWindow.WorkStatus.Plan_To_Arrive_By))
            {
                //Here present only one Date
                procureContractorCrewWindow.SelectDateForPlanToArriveByWorkStatus(startingDateTime);
            }
            else
            {
                throw new AssertionException("Absent case for expected Work status");
            }

            //Assigned Supervisor and additional requinments
            procureContractorCrewWindow.AddAssignedSupervisor(settingsInputForm.AssignToSupervisor);
            procureContractorCrewWindow.EnterAdditionalRequirement(settingsInputForm.AdditionalRequirement);
            DateTime timeOfAction = DateTime.Now;
            procureContractorCrewWindow.ClickProcureCrewsButton();
            nonIouMarketplacePage.GetHeaderPanel()
                .OpenAccountDropdownMenu()
                .ClickLogoutButton();

            //E2E010
            //Check result as Contractor (From ProcurementRequestPage page, Message panel, Emails)
            MainPage mainPage = LoginUntoDefaultContractor(contractorUser);
            ProcurementRequestPage procurementRequestPage = mainPage.GetHeaderPanel().OpenWorkforceMenuPanel()
                .ClickProcurementRequest();
            procurementRequestPage.GetProcurementRequestsPanel().GetAssertionUtils().TrueAssertion("Verify that row with resource pool: " + resourcePool + " present", procurementRequestPage.GetProcurementRequestsPanel().IsRowPresentOnAnyPageByResourcePool(resourcePool));

            ////Check message
            String expectedSubject = "Procurement request from " + operatingCompanyName;
            WRMAutotests.PageObjects.Web.Contractor.panels.MessagePanel messagePanel = procurementRequestPage.GetHeaderPanel()
                .ClickMessagesButoon()
                .GetMessageCenterPanel()
                .GetEmailRowsByPartSubject(expectedSubject)[0]
                .ClickSubject();
            messagePanel.GetAssertionUtils().TrueAssertion("Verify that message contains text: " + "Resource pool: " + resourcePool, messagePanel.GetMessageText().Contains("Resource pool: " + resourcePool));

            ////check emails
            MailRepository mailRepository = new MailRepository(contractorUser);
            IList<MimeMessage> foundEmails = mailRepository.GetUnreadEmailsByPartOfSubjectAndRecivedAndAfterDateTimeAndPartOfBodyEmails(expectedSubject, contractorUser.GetEmail(), timeOfAction, "Resource pool: " + resourcePool);
            messagePanel.GetAssertionUtils().TrueAssertionWithoutNameOfPageObject("Verify that Expected Email present in the Inbox", foundEmails.Count > 0);

        }

        [TearDown]
        public void DeleteCreatedResourcePool()
        {
            BaseInformation baseInformation = AddNewDriverWithDefaultSettings();
            MainPage mainPage = LoginUntoDefaultContractor(baseInformation, contractorUser);
            mainPage.GetHeaderPanel()
                .ClickHomeButton()
                .GetHeaderPanel()
                .OpenWorkforceMenuPanel()
                .ClickInternalWorkforceButton()
                .ClickTab(tabDiscipline)
                .GetResourcesPoolPanel()
                .GetResourcePoolRowsByResourcePoolName(resourcePoolName)[0]
                .ClickOperationButton()
                .ClickDeleteButton()
                .ClickConfirmButton();
            Thread.Sleep(15000);
        }

        private class InputFormSettings
        {
            public bool ResourceAll;
            public string ResourceAmount;
            public bool BucketAll;
            public string BucketAmount;
            public bool DiggerAll;
            public string DiggerAmount;
            public string Destination;
            public string Location;
            public string WorkStatus;
            public string StartDate;
            public string EndDate;
            public string AssignToSupervisor;
            public string AdditionalRequirement;
        }

    }
}
