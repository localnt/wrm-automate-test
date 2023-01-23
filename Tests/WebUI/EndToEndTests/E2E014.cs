using Newtonsoft.Json;
using RetryOnException;
using WRMAutotests.PageObjects.Web.Contractor.pages;
using WRMAutotests.PageObjects.Web.Contractor.panels;
using WRMAutotests.PageObjects.Web.Contractor.windows;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.Tests.WebUI.EndToEndTests
{
    public class E2E014 : BaseEndToEndTest
    {

        private static String operatingCompanyName = excelReadedUtils.GetCellValue(1, 16, 2).Equals("") ? defaultOperatingCompany : excelReadedUtils.GetCellValue(1, 16, 2);
        private static String eventName = excelReadedUtils.GetCellValue(1, 16, 3).Equals("") ? defaultEvent : excelReadedUtils.GetCellValue(1, 16, 3);
        private static String discipline = excelReadedUtils.GetCellValue(1, 16, 4).Equals("") ? defaultDiscipline : excelReadedUtils.GetCellValue(1, 16, 4);

        private static InputFormForE2E016Test settingsInputFormForE2E016 = JsonConvert.DeserializeObject<InputFormForE2E016Test>(excelReadedUtils.GetCellValue(1, 18, 5));
        private static InputFormForE2E017Test settingsInputFormForE2E017 = JsonConvert.DeserializeObject<InputFormForE2E017Test>(excelReadedUtils.GetCellValue(1, 19, 5));
        private String resourcePoolName = GetRandomValuesUtilities().GetRandomValue();
        private InternalWorkforcePage.Tabs tabDiscipline = InternalWorkforcePage.GetTabByNameFoTab(discipline);
        private String resourcePool;

        [SetUp]
        public void CreateNeedEnteties()
        {
            int estimatedResources = 5;
            int estimatedCrews = 3;
            int crewSize = 4;
            int estimatedBuckets = 6;
            int estimatedDiggers = 6;

            BaseInformation baseInformationForContractor = AddNewDriverWithDefaultSettings();
            MainPage mainPage = LoginUntoDefaultContractor(baseInformationForContractor, contractorUser);
            InternalWorkforcePage internalWorkforcePage = CreateResourcePool(mainPage, tabDiscipline, resourcePoolName, defaultSourceLocation, estimatedResources, estimatedCrews, crewSize, estimatedBuckets, estimatedDiggers, checkIouCheckbox, operatingCompanyName);
            internalWorkforcePage = CreateCrewAvailabilityForm(internalWorkforcePage, resourcePoolName, defaultOperatingCompany);

            BaseInformation baseInformationForUtility = AddNewDriverWithDefaultSettings();
            ManageSecuredWorkforcePage manageSecuredWorkforcePage = LoginIntoDefaultUtility(baseInformationForUtility, utilityUser, defaultOperatingCompany, eventName);
            resourcePool = CreateProcureCrew(manageSecuredWorkforcePage, defaultSourceLocation, resourcePoolName, "Adam Turner");
            RemoveDriver(baseInformationForUtility);

            ProcurementRequestPage procurementRequestPage = internalWorkforcePage.GetHeaderPanel()
                .OpenWorkforceMenuPanel()
                .ClickProcurementRequest();
            AcceptProcurenmentRequest(procurementRequestPage, resourcePool);
            RemoveDriver(baseInformationForContractor);
        }

        [RetryOnException(ListOfExceptions = new[] { typeof(Exception) })]
        [Retry(numberOfTryFroWebTests)]
        [Test]
        public void E2E014_Test()
        {
            MainPage mainPage = LoginUntoDefaultContractor(contractorUser);
            mainPage = mainPage.GetHeaderPanel()
                .ClickHomeButton();
            InternalWorkforcePage internalWorkforcePage = mainPage.GetHeaderPanel()
                .OpenWorkforceMenuPanel()
                .ClickInternalWorkforceButton();

            //E2E015
            internalWorkforcePage = internalWorkforcePage.ClickTab(tabDiscipline);
            ResourcePoolEditOrganizationPage resourcePoolEditOrganizationPage = internalWorkforcePage.GetResourcesPoolPanel()
                .GetResourcePoolRowsByResourcePoolName(resourcePoolName)[0]
                .ClickResourcePool()
                .ClickEditOrganizationButton();

            //E2E016
            String resourceFirstName = settingsInputFormForE2E016.FirstName + GetRandomValuesUtilities().GenerateRandomNumber(0, 9999);
            String resourceLastName = settingsInputFormForE2E016.LastName + GetRandomValuesUtilities().GenerateRandomNumber(0, 9999); ;
            String classification = settingsInputFormForE2E016.Classification;
            String fullResourceName = resourceFirstName + " " + resourceLastName;
            GetDefaultBaseInformation().GetDriver().Navigate().Refresh();
            AddResourceWindow addresourceWindow = resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel().ClickAddResourceButton();
            addresourceWindow.EnterFirstName(resourceFirstName);
            addresourceWindow.EnterLastName(resourceLastName);
            if (!settingsInputFormForE2E016.Union.Equals(""))
            {
                addresourceWindow.SelectUnion(settingsInputFormForE2E016.Union);
            }
            addresourceWindow.SelectClassification(classification);
            if (settingsInputFormForE2E016.Gender.ToLower().Equals("male"))
            {
                addresourceWindow.ClickMaleButton();
            }
            else if (settingsInputFormForE2E016.Gender.ToLower().Equals("female"))
            {
                addresourceWindow.ClickFemaleButton();
            }
            else
            {
                throw new AssertionException("Unexpected Gender: " + settingsInputFormForE2E016.Gender);
            }
            addresourceWindow.ClickAddResource();

            resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel()
                .GetAssertionUtils()
                .TrueAssertion("Verify that resource row with name: " + fullResourceName + " present", resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel().GetResourceRowsByResourceName(fullResourceName).Count == 0);

            //E2E017
            String type = settingsInputFormForE2E017.Type;
            String subType = settingsInputFormForE2E017.Subtype;
            String licensePlate = settingsInputFormForE2E017.LicensePlate + GetRandomValuesUtilities().GenerateRandomNumber(0, 9999);
            String LicenseState = settingsInputFormForE2E017.LicenseState;
            String equipmentId = settingsInputFormForE2E017.EquipmentID + GetRandomValuesUtilities().GenerateRandomNumber(0, 9999);
            resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel()
                .ClickEquimentTab();
            AddEquipmentWindow addEquipmentWindow = resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel().ClickAddEquipmentButton();
            addEquipmentWindow.SelectType(type);
            addEquipmentWindow.SelectSubType(subType);
            addEquipmentWindow.EnterLicensePlate(licensePlate);
            addEquipmentWindow.SelectLicenseState(LicenseState);
            addEquipmentWindow.EnterEquipmentId(equipmentId);
            addEquipmentWindow.ClickAddEquipment();

            resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel()
                .GetAssertionUtils()
                .TrueAssertion("Verify that equipmet row with licens plate: " + licensePlate + " present",
            resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel().GetEquipmentRowByLicensePlateEquipmentId(licensePlate).Count > 0);

            //E2E018
            //drag and drop resource
            int numberOfRowForOrganization = 0;
            if (classification.Equals("Damage Assessor"))
            {
                numberOfRowForOrganization = 3;
            }
            else if (classification.Equals("Team Lead"))
            {
                numberOfRowForOrganization = 0;
            }
            else if (classification.Equals("Area Field Manager"))
            {
                numberOfRowForOrganization = 1;
            }
            else if (classification.Equals("Driver"))
            {
                numberOfRowForOrganization = 2;
            }
            else if (classification.Equals("Apprentice Assesor"))
            {
                numberOfRowForOrganization = 4;
            }

            ResourcePoolEditOrganizationPanel resourcePoolEditOrganizationPanel = resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel();
            resourcePoolEditOrganizationPanel.ClickResourceTab();
            resourcePoolEditOrganizationPanel.SearchResource(fullResourceName);
            ResourcePoolEditOrganizationPanel.ResourceRow resourceRow = resourcePoolEditOrganizationPanel.GetResourceRowsByResourceName(fullResourceName)[0];
            ResourcePoolEditOrganizationPanel.OrganizationRow organizationRow = resourcePoolEditOrganizationPanel.GetOrganizationRows()[numberOfRowForOrganization];
            resourcePoolEditOrganizationPanel.DragResourceIntoOrganization(resourceRow, organizationRow);

            //drag and drop equpment
            resourcePoolEditOrganizationPanel = resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel();
            resourcePoolEditOrganizationPanel.ClickEquimentTab();
            resourcePoolEditOrganizationPanel.SearchEqupment(licensePlate);
            ResourcePoolEditOrganizationPanel.EquipmentRow equipmentRow = resourcePoolEditOrganizationPanel.GetEquipmentRowByLicensePlateEquipmentId(licensePlate)[0];
            organizationRow = resourcePoolEditOrganizationPanel.GetOrganizationRows()[numberOfRowForOrganization];
            resourcePoolEditOrganizationPanel.DragEqupmentIntoOrganization(equipmentRow, organizationRow);

            //E2E019
            resourcePoolEditOrganizationPanel.ClickSaveButton();
            //Check that organization updated
            String expectedFullNameOfResource = fullResourceName + " (" + classification + ")";
            organizationRow = resourcePoolEditOrganizationPanel.GetOrganizationRows()[numberOfRowForOrganization];
            organizationRow.GetAssertionUtils().EquialAssertion("Verify that for organization position present new name: " + expectedFullNameOfResource, expectedFullNameOfResource, organizationRow.GetName());
            organizationRow.GetAssertionUtils().TrueAssertion("Verify that for organization position with name: " + expectedFullNameOfResource + " present equipment" + expectedFullNameOfResource, organizationRow.IsEquipmentPresent());

        }

        private class InputFormForE2E016Test
        {
            public String FirstName;
            public String LastName;
            public String Union;
            public String Classification;
            public String Gender;
        }

        private class InputFormForE2E017Test
        {
            public String Type;
            public String Subtype;
            public String LicensePlate;
            public String LicenseState;
            public String EquipmentID;
        }



    }
}
