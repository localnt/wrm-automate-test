using MimeKit;
using RetryOnException;
using WRMAutotests.PageObjects.Web.Contractor.pages;
using WRMAutotests.PageObjects.Web.Contractor.windows.crewavailabilitywindow;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.Tests.WebUI.EndToEndTests
{
    public class E2E007 : BaseEndToEndTest
    {

        private static String operatingCompanyName = excelReadedUtils.GetCellValue(1, 9, 2).Equals("") ? defaultOperatingCompany : excelReadedUtils.GetCellValue(1, 9, 2);
        private static String eventName = excelReadedUtils.GetCellValue(1, 9, 3).Equals("") ? defaultEvent : excelReadedUtils.GetCellValue(1, 9, 3);
        private static String discipline = excelReadedUtils.GetCellValue(1, 9, 4).Equals("") ? defaultDiscipline : excelReadedUtils.GetCellValue(1, 8, 4);

        private String resourcePoolName = GetRandomValuesUtilities().GetRandomValue();
        private InternalWorkforcePage.Tabs tabDiscipline = InternalWorkforcePage.GetTabByNameFoTab(discipline);

        [SetUp]
        public void CreateResourcePool()
        {
            String sourceLocation = "Aaronsburg, PA";
            int estimatedResources = 5;
            int estimatedCrews = 3;
            int crewSize = 4;
            int estimatedBuckets = 6;
            int estimatedDiggers = 6;

            BaseInformation baseInformation = AddNewDriverWithDefaultSettings();
            MainPage mainPage = LoginUntoDefaultContractor(baseInformation, contractorUser);
            InternalWorkforcePage internalWorkforcePage = CreateResourcePool(mainPage, tabDiscipline, resourcePoolName, sourceLocation, estimatedResources, estimatedCrews, crewSize, estimatedBuckets, estimatedDiggers, checkIouCheckbox, operatingCompanyName);
            RemoveDriver(baseInformation);
        }

        [RetryOnException(ListOfExceptions = new[] { typeof(Exception) })]
        [Retry(numberOfTryFroWebTests)]
        [Test]
        public void E2E007_Test()
        {
            //Share Crew Availability Form
            MainPage mainPage = LoginUntoDefaultContractor(contractorUser);
            InternalWorkforcePage internalWorkforcePage = mainPage.GetHeaderPanel()
                .ClickHomeButton()
                .GetHeaderPanel()
                .OpenWorkforceMenuPanel()
                .ClickInternalWorkforceButton()
                .ClickTab(tabDiscipline);
            String resourcePool = internalWorkforcePage.GetResourcesPoolPanel()
                .GetResourcePoolRowsByResourcePoolName(resourcePoolName)[0]
                .GetResourcePool();
            CrewAvailabilityFormWindow crewAvailabilityFormWindow = internalWorkforcePage.ClickCrewAvailabilityFormButton();
            crewAvailabilityFormWindow.GetResourcePoolTablePanel()
                .GetRowsByResourcePool(resourcePool)[0]
                .ClickCheckbox();
            crewAvailabilityFormWindow.GetUtilitiesTablePanel()
                .GetRowsByUtilityName(operatingCompanyName)[0]
                .ClickCheckbox();
            DateTime timeOfAction = DateTime.Now;
            crewAvailabilityFormWindow.ClickShareCrewAvailability();
            internalWorkforcePage.GetHeaderPanel()
                .OpenAccountDropdownMenu()
                .ClickLogoutButton();

            //Check from utility user
            DefaultPage defaultPage = OpenDefaultUtilityPage();
            WRMAutotests.PageObjects.Web.Utility.pages.LoginPage loginPage = defaultPage.ClickLoginButton();
            loginPage.GetLoginPanel().EnterEmail(utilityUser.GetEmail());
            loginPage.GetLoginPanel().EnterPassword(utilityUser.GetPassword());
            EventSelectionPage eventSelectionPage = loginPage.GetLoginPanel().ClickLoginButton();
            ManageSecuredWorkforcePage manageSecuredWorkforcePage = eventSelectionPage.GetWelcomeToStormManagerPanel()
                .ClickAccessWithoutEventSelection();
            manageSecuredWorkforcePage.GetHeaderPanel()
                .OpenOperatingCompanyDropdownMenu()
                .ClickOperatingCompanyByName(operatingCompanyName);
            NonIouMarketplacePage nonIouMarketplacePage = manageSecuredWorkforcePage.GetHeaderPanel()
                .OpenWorkforceEventsManuPanel()
                .ClickNonIouMarketplaceButton();

            nonIouMarketplacePage.GetCrewAvailabilitiesPanel().GetAssertionUtils().TrueAssertion("Verify that Row with resource pool: " + resourcePool + " present on any page", nonIouMarketplacePage.GetCrewAvailabilitiesPanel().IsRowPresentOnAnyPageByResourcePool(resourcePool));

            //Test E2E008
            //check message from Message center panel
            String expectedSubject = "Notification of crew availability information updates from Best's Line&Company to your company";
            WRMAutotests.PageObjects.Web.Utility.panel.MessageCenterPanel messageCenterPanel = nonIouMarketplacePage.GetHeaderPanel()
                .ClickMessagesButoon()
                .GetMessageCenterPanel();
            WRMAutotests.PageObjects.Web.Utility.panel.MessagePanel messagePanel = messageCenterPanel.GetUnreadEmailRowsByPartSubject(expectedSubject)[0]
                .ClickSubject();

            messagePanel.GetAssertionUtils().TrueAssertion("Verify that date of message expected or more: " + timeOfAction, messagePanel.GetDate().AddHours(timeZoneDifferenceHours) >= timeOfAction);

            //check message from Email
            MailRepository mailRepository = new MailRepository(utilityUser);
            IList<MimeMessage> foundEmails = mailRepository.GetUnreadEmailsByPartOfSubjectAndRecivedAndAfterDateTimeEmails("Notification of crew availability information updates from  Best's Line&Company to your company", utilityUser.GetEmail(), timeOfAction);
            messagePanel.GetAssertionUtils().TrueAssertionWithoutNameOfPageObject("Verify that expected Email recived", foundEmails.Count > 0);
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
