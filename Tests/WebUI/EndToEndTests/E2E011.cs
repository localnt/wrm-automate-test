using RetryOnException;
using WRMAutotests.PageObjects.Web.Contractor.pages;
using WRMAutotests.PageObjects.Web.Contractor.panels;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.Tests.WebUI.EndToEndTests
{
    public class E2E011 : BaseEndToEndTest
    {

        private static String operatingCompanyName = excelReadedUtils.GetCellValue(1, 13, 2).Equals("") ? defaultOperatingCompany : excelReadedUtils.GetCellValue(1, 13, 2);
        private static String eventName = excelReadedUtils.GetCellValue(1, 13, 3).Equals("") ? defaultEvent : excelReadedUtils.GetCellValue(1, 13, 3);
        private static String discipline = excelReadedUtils.GetCellValue(1, 13, 4).Equals("") ? defaultDiscipline : excelReadedUtils.GetCellValue(1, 13, 4);

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

            BaseInformation baseInformation = AddNewDriverWithDefaultSettings();
            MainPage mainPage = LoginUntoDefaultContractor(baseInformation, contractorUser);
            InternalWorkforcePage internalWorkforcePage = CreateResourcePool(mainPage, tabDiscipline, resourcePoolName, defaultSourceLocation, estimatedResources, estimatedCrews, crewSize, estimatedBuckets, estimatedDiggers, checkIouCheckbox, operatingCompanyName);
            internalWorkforcePage = CreateCrewAvailabilityForm(internalWorkforcePage, resourcePoolName, operatingCompanyName);
            RemoveDriver(baseInformation);
            baseInformation = AddNewDriverWithDefaultSettings();
            ManageSecuredWorkforcePage manageSecuredWorkforcePage = LoginIntoDefaultUtility(baseInformation, utilityUser, operatingCompanyName, eventName);
            resourcePool = CreateProcureCrew(manageSecuredWorkforcePage, defaultSourceLocation, resourcePoolName, "Adam Turner");
            RemoveDriver(baseInformation);
        }


        [RetryOnException(ListOfExceptions = new[] { typeof(Exception) })]
        [Retry(numberOfTryFroWebTests)]
        [Test]
        public void E2E011_Test()
        {
            MainPage mainPage = LoginUntoDefaultContractor(contractorUser);
            ProcurementRequestPage procurementRequestPage = mainPage.GetHeaderPanel()
                .OpenWorkforceMenuPanel()
                .ClickProcurementRequest();
            ProcurementRequestsPanel.Row row = procurementRequestPage.GetProcurementRequestsPanel()
                .GetRowsFromCurrentPageByResourcePool(resourcePool)[0];

            row.GetAssertionUtils().EquialAssertion("Verify that for row with resource pool: " + resourcePool + " . Present status : " + "Waiting for Review", "Waiting for Review", row.GetStatus());

            row.ClickEditButton()
                .ClickAcceptButton()
                .ClickConfirmButton();
            procurementRequestPage.GetHeaderPanel()
                .OpenAccountDropdownMenu()
                .ClickLogoutButton();

            //E2E012
            ManageSecuredWorkforcePage manageSecuredWorkforcePage = LoginIntoDefaultUtility(utilityUser);
            NonIouMarketplacePage nonIouMarketplacePage = manageSecuredWorkforcePage.GetHeaderPanel()
                .OpenWorkforceEventsManuPanel()
                .ClickNonIouMarketplaceButton();
            nonIouMarketplacePage.GetHeaderPanel().OpenOperatingCompanyDropdownMenu()
                .ClickOperatingCompanyByName(operatingCompanyName);

            nonIouMarketplacePage.GetCrewAvailabilitiesPanel()
                .GetAssertionUtils()
                .TrueAssertion("Verify that for row with resource pool: " + resourcePool + " present Accepted label",
                nonIouMarketplacePage.GetCrewAvailabilitiesPanel()
                .GetRowsByResourcePoolFromAnyPage(resourcePool)[0]
                .IsRequestAcceptedLabelPresent());


            //E2E013
            manageSecuredWorkforcePage = nonIouMarketplacePage.GetHeaderPanel()
                .OpenWorkforceEventsManuPanel()
                .ClickManageSecuredWorkforce();
            int foundNumberPfRows = manageSecuredWorkforcePage.GetSecuredWorkforcesPanel().GetNumberOFRowsByResourcePoolFromAllPages(resourcePool);
            manageSecuredWorkforcePage.GetSecuredWorkforcesPanel()
                .GetAssertionUtils()
                .TrueAssertion("Verify that number of rows with resource pool: " + resourcePool + " more than 0", foundNumberPfRows > 0);
            manageSecuredWorkforcePage.GetSecuredWorkforcesPanel()
                .GetAssertionUtils()
                .TrueAssertion("Verify that number of rows with resource pool: " + resourcePool + " equal to 1", foundNumberPfRows == 1);
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

    }

}
