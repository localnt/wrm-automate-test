using NUnit.Allure.Core;
using WRMAutotests.PageObjects.Web.Contractor.pages;
using WRMAutotests.PageObjects.Web.Contractor.panels;
using WRMAutotests.PageObjects.Web.Contractor.windows;
using WRMAutotests.PageObjects.Web.Contractor.windows.crewavailabilitywindow;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.PageObjects.Web.Utility.windows;
using WRMAutotests.Tests.StabilityScript;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

[assembly: LevelOfParallelism(4)]

namespace WRMAutotests.Tests.WebUI.Base
{
    [Parallelizable(scope: ParallelScope.All)]
    [AllureNUnit]
    [TestFixture]
    public class BaseWebTest : BaseTest
    {
        //this need for multy thread test run
        //we use List instead value for situation when we need more than one driver for test
        private ThreadLocal<IList<BaseInformation>> baseInformations = new ThreadLocal<IList<BaseInformation>>();

        private DriverUtils driverUtils = new DriverUtils();

        private String defaultContractorUrl = excelReadedUtils.GetCellValue(0, 2, 1);
        private String defaultUtilityUrl = excelReadedUtils.GetCellValue(0, 3, 1);
        public User contractorUser = new User(excelReadedUtils.GetCellValue(0, 2, 2), excelReadedUtils.GetCellValue(0, 2, 3), excelReadedUtils.GetCellValue(0, 2, 4));
        public User utilityUser = new User(excelReadedUtils.GetCellValue(0, 3, 2), excelReadedUtils.GetCellValue(0, 3, 3), excelReadedUtils.GetCellValue(0, 3, 4));

        public BaseInformation GetDefaultBaseInformation()
        {
            return GetBaseInformation()[0];
        }

        public IList<BaseInformation> GetBaseInformation()
        {
            return baseInformations.Value;
        }

        public BaseInformation AddNewDriverWithDefaultSettings()
        {
            BaseInformation baseInformation = new BaseInformation(driverUtils.GenerateDefaultWebDriver(), makeScreensootForEveryReportStep);
            baseInformations.Value.Add(baseInformation);
            return baseInformation;
        }

        public void RemoveDriver(BaseInformation baseInformation)
        {
            baseInformations.Value.Remove(baseInformation);
            DriverUtils.CloseDriver(baseInformation.GetDriver());
        }

        [SetUp]
        public void SetUpDriver()
        {
            //create 1 default driver because in any case for any web test we should have at least 1 driver
            baseInformations.Value = new List<BaseInformation>();
            AddNewDriverWithDefaultSettings();
        }

        [TearDown]
        public void CloseDrive()
        {
            foreach (BaseInformation baseInformation in GetBaseInformation())
            {
                //add screeshoot after finishing test
                try
                {
                    ReportUtils.MakeScreenshoot(baseInformation);
                }
                catch (Exception ex)
                {

                }

                //close driver
                DriverUtils.CloseDriver(baseInformation.GetDriver());
            }

        }

        public void OpenUrl(BaseInformation baseInformation, String url)
        {
            baseInformation.GetDriver().Navigate().GoToUrl(url);
        }

        public void OpenUrl(String url)
        {
            OpenUrl(GetDefaultBaseInformation(), url);
        }

        public WRMAutotests.PageObjects.Web.Contractor.pages.LoginPage OpenLoginPageForContractor()
        {
            return OpenLoginPageForContractor(GetDefaultBaseInformation());
        }
        public WRMAutotests.PageObjects.Web.Contractor.pages.LoginPage OpenLoginPageForContractor(BaseInformation baseInformation)
        {
            OpenUrl(baseInformation, defaultContractorUrl);
            return new WRMAutotests.PageObjects.Web.Contractor.pages.LoginPage(baseInformation);
        }

        public DefaultPage OpenDefaultUtilityPage(BaseInformation baseInformation)
        {
            OpenUrl(baseInformation, defaultUtilityUrl);
            return new DefaultPage(baseInformation);
        }

        public DefaultPage OpenDefaultUtilityPage()
        {
            return OpenDefaultUtilityPage(GetDefaultBaseInformation());
        }

        public ManageSecuredWorkforcePage LoginIntoDefaultUtility(BaseInformation baseInformation, User user)
        {
            DefaultPage defaultPage = OpenDefaultUtilityPage(baseInformation);
            WRMAutotests.PageObjects.Web.Utility.pages.LoginPage loginPage = defaultPage.ClickLoginButton();
            loginPage.GetLoginPanel().EnterEmail(user.GetEmail());
            loginPage.GetLoginPanel().EnterPassword(user.GetPassword());
            EventSelectionPage eventSelectionPage = loginPage.GetLoginPanel().ClickLoginButton();
            return eventSelectionPage.GetWelcomeToStormManagerPanel()
                .ClickAccessWithoutEventSelection();

        }

        public ManageSecuredWorkforcePage LoginIntoDefaultUtility(User user)
        {
            return LoginIntoDefaultUtility(GetDefaultBaseInformation(), user);
        }

        public ManageSecuredWorkforcePage LoginIntoDefaultUtility(BaseInformation baseInformation, User user, String operationCompany, String eventName)
        {
            DefaultPage defaultPage = OpenDefaultUtilityPage(baseInformation);
            WRMAutotests.PageObjects.Web.Utility.pages.LoginPage loginPage = defaultPage.ClickLoginButton();
            loginPage.GetLoginPanel().EnterEmail(user.GetEmail());
            loginPage.GetLoginPanel().EnterPassword(user.GetPassword());
            EventSelectionPage eventSelectionPage = loginPage.GetLoginPanel().ClickLoginButton();
            eventSelectionPage.GetWelcomeToStormManagerPanel().SelectOperatingCompany(operationCompany);
            return eventSelectionPage.GetWelcomeToStormManagerPanel().SelectEvent(eventName);

        }

        public ManageSecuredWorkforcePage LoginIntoDefaultUtility(User user, String operationCompany, String eventName)
        {
            return LoginIntoDefaultUtility(GetDefaultBaseInformation(), user, operationCompany, eventName);
        }

        public MainPage LoginUntoDefaultContractor(BaseInformation baseInfortamtion, User user)
        {
            WRMAutotests.PageObjects.Web.Contractor.pages.LoginPage loginPage = OpenLoginPageForContractor(baseInfortamtion);
            loginPage.EnterEmail(user.GetEmail());
            loginPage.EnterPassword(user.GetPassword());
            return loginPage.ClickLoginButton();
        }

        public MainPage LoginUntoDefaultContractor(User user)
        {
            return LoginUntoDefaultContractor(GetDefaultBaseInformation(), user);
        }

        public void CreateCrewAvailabilityRequest(ManageSecuredWorkforcePage manageSecuredWorkforcePage, String operatingCompany, String contractor, String subject, String message, String discipline)
        {
            manageSecuredWorkforcePage.GetHeaderPanel()
                .OpenOperatingCompanyDropdownMenu()
                .ClickOperatingCompanyByName(operatingCompany);
            CreawAvailabilityRequestPage creawAvailabilityRequestPage = manageSecuredWorkforcePage.GetHeaderPanel()
                .OpenWorkforceEventsManuPanel()
                .ClickCrewAvailabilityRequestButton()
                .ClickTabByName(discipline);
            creawAvailabilityRequestPage.GetCrewAvailabilityRequestContractorsTablePanel()
                .GetRowByContractorFromAnyPage(contractor)
                .ClickCheckbox();
            CrewAvailabilityRequestWindow crewAvailabilityRequestWindow = creawAvailabilityRequestPage.ClickRequestCrewAvailabilityInfo();
            crewAvailabilityRequestWindow.EnterSubject(subject);
            crewAvailabilityRequestWindow.EnterMessage(message);
            crewAvailabilityRequestWindow.ClickRequestCrewAvailabilityInfo();
        }

        public InternalWorkforcePage CreateResourcePool(MainPage mainPage, InternalWorkforcePage.Tabs tab, String resourcePoolName, String sourceLocation, int estimatedResources, int estimatedCrews, int crewSize, int estimatedBuckets, int estimatedDiggers, Boolean checkOnIouCheckbox, String utilityName)
        {
            AddResourcePoolPage addResourcePoolPage = mainPage.GetHeaderPanel()
                .ClickHomeButton()
                .GetHeaderPanel()
                .OpenWorkforceMenuPanel()
                .ClickInternalWorkforceButton()
                .ClickTab(tab)
                .ClickAddButton();
            addResourcePoolPage.EnterResourcePoolName(resourcePoolName);
            addResourcePoolPage.GetSourceLocationDropDownMenu().SelectMenuElement(sourceLocation);
            if (checkOnIouCheckbox)
            {
                addResourcePoolPage.CheckOnIouCheckbox();
                addResourcePoolPage.GetUtilityDropDownMenu().SelectMenuElement(utilityName);
            }
            else
            {
                addResourcePoolPage.UncheckOnIouCheckbox();
            }

            addResourcePoolPage.EnterEstimatedResources(estimatedResources);
            addResourcePoolPage.EnterExtimatedCrews(estimatedCrews);
            addResourcePoolPage.EnterCrewSize(crewSize);
            addResourcePoolPage.EnterEstimatedBuckets(estimatedBuckets);
            addResourcePoolPage.EnterEstimatedDiggers(estimatedDiggers);
            InternalWorkforcePage internalWorkforcePage = addResourcePoolPage.ClickAddButton();
            return internalWorkforcePage;
        }

        public InternalWorkforcePage CreateCrewAvailabilityForm(InternalWorkforcePage internalWorkforcePage, String resourcePoolName, String operatingCompany)
        {
            String resourcePool = internalWorkforcePage.GetResourcesPoolPanel()
                .GetResourcePoolRowsByResourcePoolName(resourcePoolName)[0]
                .GetResourcePool();
            CrewAvailabilityFormWindow crewAvailabilityFormWindow = internalWorkforcePage.ClickCrewAvailabilityFormButton();
            crewAvailabilityFormWindow.GetResourcePoolTablePanel()
                .GetRowsByResourcePool(resourcePool)[0]
                .ClickCheckbox();
            crewAvailabilityFormWindow.GetUtilitiesTablePanel()
                .GetRowsByUtilityName(operatingCompany)[0]
                .ClickCheckbox();
            crewAvailabilityFormWindow.ClickShareCrewAvailability();
            return internalWorkforcePage;
        }

        public String CreateProcureCrew(ManageSecuredWorkforcePage manageSecuredWorkforcePage, String sourceLocation, String resourcePoolName, String assignedSupervisor)
        {
            NonIouMarketplacePage nonIouMarketplacePage = manageSecuredWorkforcePage.GetHeaderPanel()
                .OpenWorkforceEventsManuPanel()
                .ClickNonIouMarketplaceButton();
            nonIouMarketplacePage.GetLocationDropDownMenu()
                .SelectMenuElement(sourceLocation);
            nonIouMarketplacePage.ClickUpdateButton();
            WRMAutotests.PageObjects.Web.Utility.panel.CrewAvailabilitiesPanel.Row targetrow = nonIouMarketplacePage.GetCrewAvailabilitiesPanel()
                .GetRowFromAnyPageByResourcePoolName(resourcePoolName);
            String resourcePool = targetrow.GetResourcePool();
            targetrow.ClickCheckbox();
            ProcureContractorCrewWindow procureContractorCrewWindow = nonIouMarketplacePage.ClickProcureCrew();
            procureContractorCrewWindow.ClickAllResourcesCheckbox();
            procureContractorCrewWindow.ClickAllBucketCheckbox();
            procureContractorCrewWindow.ClickAllDiggersCheckbox();
            procureContractorCrewWindow.SelectDestination(sourceLocation);
            procureContractorCrewWindow.SelectWorkStatus(ProcureContractorCrewWindow.WorkStatus.Immediatly_Mobilize);
            procureContractorCrewWindow.AddAssignedSupervisor(assignedSupervisor);
            procureContractorCrewWindow.EnterAdditionalRequirement(GetRandomValuesUtilities().GetRandomValue());
            procureContractorCrewWindow.ClickProcureCrewsButton();
            Thread.Sleep(10000);
            return resourcePool;
        }

        public void AcceptProcurenmentRequest(ProcurementRequestPage procurementRequestPage, String resourcePool)
        {
            ProcurementRequestsPanel.Row row = procurementRequestPage.GetProcurementRequestsPanel()
               .GetRowsFromCurrentPageByResourcePool(resourcePool)[0];
            row.ClickEditButton()
                .ClickAcceptButton()
                .ClickConfirmButton();
            Thread.Sleep(10000);
        }

        public ResourcePoolEditOrganizationPage AddResourceToOrganization(ResourcePoolEditOrganizationPage resourcePoolEditOrganizationPage, String resourceFirstName, String resourceLastName, String classification)
        {
            String fullResourceName = resourceFirstName + " " + resourceLastName;
            GetDefaultBaseInformation().GetDriver().Navigate().Refresh();
            AddResourceWindow addresourceWindow = resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel().ClickAddResourceButton();
            addresourceWindow.EnterFirstName(resourceFirstName);
            addresourceWindow.EnterLastName(resourceLastName);
            addresourceWindow.SelectClassification(classification);
            addresourceWindow.ClickAddResource();
            return resourcePoolEditOrganizationPage;
        }

        public ResourcePoolEditOrganizationPage AddEquipmentToOrganization(ResourcePoolEditOrganizationPage resourcePoolEditOrganizationPage, String type, String subType, String licensePlate, String LicenseState, String equipmentId)
        {
            resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel()
                .ClickEquimentTab();
            AddEquipmentWindow addEquipmentWindow = resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel().ClickAddEquipmentButton();
            addEquipmentWindow.SelectType(type);
            addEquipmentWindow.SelectSubType(subType);
            addEquipmentWindow.EnterLicensePlate(licensePlate);
            addEquipmentWindow.SelectLicenseState(LicenseState);
            addEquipmentWindow.EnterEquipmentId(equipmentId);
            addEquipmentWindow.ClickAddEquipment();
            return resourcePoolEditOrganizationPage;
        }

        public ResourcePoolEditOrganizationPage DragAndDropResourceToOrganization(ResourcePoolEditOrganizationPage resourcePoolEditOrganizationPage, String fullResourceName, int numberOfORganizationLevel)
        {
            ResourcePoolEditOrganizationPanel resourcePoolEditOrganizationPanel = resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel();
            resourcePoolEditOrganizationPanel.ClickResourceTab();
            resourcePoolEditOrganizationPanel.SearchResource(fullResourceName);
            ResourcePoolEditOrganizationPanel.ResourceRow resourceRow = resourcePoolEditOrganizationPanel.GetResourceRowsByResourceName(fullResourceName)[0];
            ResourcePoolEditOrganizationPanel.OrganizationRow organizationRow = resourcePoolEditOrganizationPanel.GetOrganizationRows()[numberOfORganizationLevel];
            resourcePoolEditOrganizationPanel.DragResourceIntoOrganization(resourceRow, organizationRow);
            return resourcePoolEditOrganizationPage;
        }

        public ResourcePoolEditOrganizationPage DragAndDropEqupment(ResourcePoolEditOrganizationPage resourcePoolEditOrganizationPage, String licensePlate, int numberOfORganizationLevel)
        {
            ResourcePoolEditOrganizationPanel resourcePoolEditOrganizationPanel = resourcePoolEditOrganizationPage.GetResourcePoolEditOrganizationPanel();
            resourcePoolEditOrganizationPanel.ClickEquimentTab();
            resourcePoolEditOrganizationPanel.SearchEqupment(licensePlate);
            ResourcePoolEditOrganizationPanel.EquipmentRow equipmentRow = resourcePoolEditOrganizationPanel.GetEquipmentRowByLicensePlateEquipmentId(licensePlate)[0];
            ResourcePoolEditOrganizationPanel.OrganizationRow organizationRow = resourcePoolEditOrganizationPanel.GetOrganizationRows()[numberOfORganizationLevel];
            resourcePoolEditOrganizationPanel.DragEqupmentIntoOrganization(equipmentRow, organizationRow);
            return resourcePoolEditOrganizationPage;
        }






    }
}

[SetUpFixture]
public class RootFixtureSetup
{
    private static ExcelReadedUtils excelReadedUtils = new ExcelReadedUtils("configDemo1.4.xlsx");
    private Boolean runWarmUpScripts = Boolean.Parse(excelReadedUtils.GetCellValue(0, 7, 1));
    private String defaultContractorUrl = excelReadedUtils.GetCellValue(0, 2, 1);
    private String defaultUtilityUrl = excelReadedUtils.GetCellValue(0, 3, 1);
    private User contractorUser = new User(excelReadedUtils.GetCellValue(0, 2, 2), excelReadedUtils.GetCellValue(0, 2, 3), excelReadedUtils.GetCellValue(0, 2, 4));
    private User utilityUser = new User(excelReadedUtils.GetCellValue(0, 3, 2), excelReadedUtils.GetCellValue(0, 3, 3), excelReadedUtils.GetCellValue(0, 3, 4));

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {

        DriverUtils driverUtils = new DriverUtils();

        if (runWarmUpScripts)
        {
            {
                BaseInformation baseInformation = new BaseInformation(driverUtils.GenerateDefaultWebDriver());
                try
                {
                    StabilityScripts.DEV_1_MT_ContractorAllMenu(baseInformation, defaultContractorUrl, contractorUser);
                }
                finally
                {
                    if (baseInformation.GetDriver() != null)
                    {
                        baseInformation.GetDriver().Close();
                    }
                }
            }
            {
                BaseInformation baseInformation = new BaseInformation(driverUtils.GenerateDefaultWebDriver());
                try
                {
                    StabilityScripts.DEV_2_NG_SCS_ALL_MENU(baseInformation, defaultUtilityUrl, utilityUser);
                }
                finally
                {
                    if (baseInformation.GetDriver() != null)
                    {
                        baseInformation.GetDriver().Close();
                    }
                }
            }
        }

    }



}


