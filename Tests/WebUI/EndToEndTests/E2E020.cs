using RetryOnException;
using WRMAutotests.PageObjects.Web.Contractor.pages;
using WRMAutotests.PageObjects.Web.Contractor.panels;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.PageObjects.Web.Utility.panel;
using WRMAutotests.PageObjects.Web.Utility.windows;
using WRMAutotests.Utility.Web;
using static WRMAutotests.PageObjects.Web.Utility.pages.ManageSecuredWorkforcePage;

namespace WRMAutotests.Tests.WebUI.EndToEndTests
{
    public class E2E020 : BaseEndToEndTest
    {

        private static String operatingCompanyName = excelReadedUtils.GetCellValue(1, 22, 2).Equals("") ? defaultOperatingCompany : excelReadedUtils.GetCellValue(1, 22, 2);
        private static String eventName = excelReadedUtils.GetCellValue(1, 22, 3).Equals("") ? defaultEvent : excelReadedUtils.GetCellValue(1, 22, 3);
        private static String discipline = excelReadedUtils.GetCellValue(1, 22, 4).Equals("") ? defaultDiscipline : excelReadedUtils.GetCellValue(1, 22, 4);

        private String resourcePoolName = GetRandomValuesUtilities().GetRandomValue();
        private InternalWorkforcePage.Tabs tabDiscipline = InternalWorkforcePage.GetTabByNameFoTab(discipline);
        private String resourcePool;
        private String fullResourceName;
        private String supervisor = "Adam Turner";
        private String timeSheetLastSubmitterName = "Manual Best";
        private String expenseLastSubmitter = "ALead01 Test";

        [SetUp]
        public void PrepareEnv()
        {
            int estimatedResources = 5;
            int estimatedCrews = 3;
            int crewSize = 4;
            int estimatedBuckets = 6;
            int estimatedDiggers = 6;

            BaseInformation baseInformationForContractor = AddNewDriverWithDefaultSettings();
            MainPage mainPage = LoginUntoDefaultContractor(baseInformationForContractor, contractorUser);
            InternalWorkforcePage internalWorkforcePage = CreateResourcePool(mainPage, tabDiscipline, resourcePoolName, defaultSourceLocation, estimatedResources, estimatedCrews, crewSize, estimatedBuckets, estimatedDiggers, checkIouCheckbox, operatingCompanyName);
            internalWorkforcePage = CreateCrewAvailabilityForm(internalWorkforcePage, resourcePoolName, operatingCompanyName);

            BaseInformation baseInformationForUtility = AddNewDriverWithDefaultSettings();
            ManageSecuredWorkforcePage manageSecuredWorkforcePage = LoginIntoDefaultUtility(baseInformationForUtility, utilityUser, operatingCompanyName, eventName);
            resourcePool = CreateProcureCrew(manageSecuredWorkforcePage, defaultSourceLocation, resourcePoolName, supervisor);
            RemoveDriver(baseInformationForUtility);

            ProcurementRequestPage procurementRequestPage = internalWorkforcePage.GetHeaderPanel()
                .OpenWorkforceMenuPanel()
                .ClickProcurementRequest();
            AcceptProcurenmentRequest(procurementRequestPage, resourcePool);

            //edit organization
            internalWorkforcePage = procurementRequestPage.GetHeaderPanel()
                .OpenWorkforceMenuPanel()
                .ClickInternalWorkforceButton();
            ResourcePoolEditOrganizationPage resourcePoolEditOrganizationPage = internalWorkforcePage.GetResourcesPoolPanel()
                .GetResourcePoolRowsByResourcePoolName(resourcePoolName)[0]
                .ClickResourcePool()
                .ClickEditOrganizationButton();
            String resourceFirstName = GetRandomValuesUtilities().GetRandomValue();
            String resourceLastName = GetRandomValuesUtilities().GetRandomValue();
            String classification = "Driver";
            fullResourceName = resourceFirstName + " " + resourceLastName;
            resourcePoolEditOrganizationPage = AddResourceToOrganization(resourcePoolEditOrganizationPage, resourceFirstName, resourceLastName, classification);

            String type = "Bucket";
            String subType = "TestBucket";
            String licensePlate = GetRandomValuesUtilities().GetRandomValue();
            String LicenseState = "AL";
            String equipmentId = GetRandomValuesUtilities().GetRandomValue();
            resourcePoolEditOrganizationPage = AddEquipmentToOrganization(resourcePoolEditOrganizationPage, type, subType, licensePlate, LicenseState, equipmentId);
            resourcePoolEditOrganizationPage = DragAndDropResourceToOrganization(resourcePoolEditOrganizationPage, fullResourceName, 4);
            resourcePoolEditOrganizationPage = DragAndDropEqupment(resourcePoolEditOrganizationPage, licensePlate, 4);
            resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel().ClickSaveButton();
            Thread.Sleep(60000);
            RemoveDriver(baseInformationForContractor);

        }

        [RetryOnException(ListOfExceptions = new[] { typeof(Exception) })]
        [Retry(numberOfTryFroWebTests)]
        [Test]
        public void E2E020_Test()
        {
            MainPage mainPage = LoginUntoDefaultContractor(contractorUser);
            mainPage = mainPage.GetHeaderPanel()
                .ClickHomeButton();
            InternalWorkforcePage internalWorkforcePage = mainPage.GetHeaderPanel()
                .OpenWorkforceMenuPanel()
                .ClickInternalWorkforceButton();
            internalWorkforcePage.ClickTab(tabDiscipline);
            ResourcePoolOverviewPage resourcePoolOverviewPage = internalWorkforcePage.GetResourcesPoolPanel()
                .GetResourcePoolRowsByResourcePoolName(resourcePoolName)[0]
                .ClickResourcePool();
            resourcePoolOverviewPage.ClickNewCrewSheetButton();

            //E2E021
            String crewSheet = resourcePoolOverviewPage.GetCrewSheetsPanel()
                .GetRowByCrewSheetName(resourcePoolName)
                .GetCrewSheet();
            resourcePoolOverviewPage.GetResourcePoolResourcesPanel()
                .GetRows()[0]
                .ClickCheckbox();
            resourcePoolOverviewPage.GetResourcePoolResourcesPanel()
                .ClickAssignButton()
                .AssignToCrewSheet(crewSheet);

            Thread.Sleep(10000);
            CrewSheetsPanel.Row crewSheetRow = resourcePoolOverviewPage.GetCrewSheetsPanel()
                .GetRowByCrewSheetName(resourcePoolName);
            resourcePoolOverviewPage.GetAssertionUtils().EquialAssertion("Verify that resources added to crew sheet", "1", crewSheetRow.GetResourceValue());
            resourcePoolOverviewPage.GetAssertionUtils().EquialAssertion("Verify that bucket added to crew sheet", "1", crewSheetRow.GetBucketValue());
            resourcePoolOverviewPage.GetAssertionUtils().EquialAssertion("Verify that Leader name expected for crew sheet", fullResourceName + " (Sp)", crewSheetRow.GetLeaderValue());

            //E2E022
            resourcePoolOverviewPage.GetCrewSheetsPanel()
                .GetRows()[0]
                .ClickCheckbox();
            resourcePoolOverviewPage.ClickSubmitButton()
                .SelectUtility(operatingCompanyName)
                .ClickConfirmButton();

            String submittedToUtilityValue = resourcePoolOverviewPage.GetCrewSheetsPanel()
                .GetRowByCrewSheetName(resourcePoolName)
                .GetSubmittedToValue();
            resourcePoolOverviewPage.GetCrewSheetsPanel()
                .GetAssertionUtils()
                .EquialAssertion("Verify that submitted To cell contain Selected Utility", operatingCompanyName, submittedToUtilityValue);

            //E2E023
            ResourcePoolEditCrewSheetPage resourcePoolEditCrewSheetPage = resourcePoolOverviewPage.GetCrewSheetsPanel()
                .GetRowByCrewSheetName(resourcePoolName)
                .ClickCrewSheetIdLink();
            resourcePoolEditCrewSheetPage.SelectTimeSheetLastSubmitter(timeSheetLastSubmitterName);
            resourcePoolEditCrewSheetPage.SelectExpenceLastSubmitter(expenseLastSubmitter);
            //TODO submit message
            //TODO check message

            //E2E024
            BaseInformation baseInformationUtility = AddNewDriverWithDefaultSettings();
            ManageSecuredWorkforcePage manageSecuredWorkforcePage = LoginIntoDefaultUtility(baseInformationUtility, utilityUser, operatingCompanyName, "All Active Events");
            manageSecuredWorkforcePage.GetHeaderPanel().OpenEventDropdownMenu().ClickEventByName("— All Include No Event —");
            SecuredWorkforcesPanel.Row foundRow = manageSecuredWorkforcePage.GetSecuredWorkforcesPanel()
                .GetRowByResourcePoolFromAmyPage(resourcePool);
            //TODO check crew sheet from row
            CrewSheetOrganizationWindow crewSheetOrganizationWindow = foundRow.ClickCrewSheetLink();
            //TODO add checks
            crewSheetOrganizationWindow.ClickCloseButton();

            //E2E025
            manageSecuredWorkforcePage.GetSecuredWorkforcesPanel()
                .GetRowByResourcePoolFromAmyPage(resourcePool)
                .ClickChecbox();
            manageSecuredWorkforcePage.ClickEventButton()
                .ApplyEvent(eventName, DateTime.Now.AddDays(-1));
            manageSecuredWorkforcePage.ClickSupervisorButton()
                .SelectSupervisorAndClickAssignButton(supervisor);

            AssignedLocation assignedLocation = manageSecuredWorkforcePage.ClickAssignedLocationButton();
            assignedLocation.ClickRegionTypeOfLocation();
            String location = "[North - Central]";
            assignedLocation.SelectLocation(location);
            assignedLocation.SelectDate(DateTime.Now.AddDays(-1));
            String etaComment = GetRandomValuesUtilities().GetRandomValue();
            assignedLocation.EnterAdditionalRequiments(etaComment);
            assignedLocation.ClickAssignButton();

            foundRow = manageSecuredWorkforcePage.GetSecuredWorkforcesPanel()
                .GetRowByResourcePoolFromAmyPage(resourcePool);
            foundRow.GetAssertionUtils().EquialAssertion("Verify that Supervisor expected", supervisor + " ()", foundRow.GetSupervisor());
            foundRow.GetAssertionUtils().EquialAssertion("Verify that Event name expected", eventName, foundRow.GetEventName());
            foundRow.GetAssertionUtils().EquialAssertion("Verify that Location expected", location, foundRow.GetAssignedLocation());
            foundRow.GetAssertionUtils().EquialAssertion("Verify that ETA comment expected", etaComment, foundRow.GetEtaComment());
        }

    }
}
