using Newtonsoft.Json;
using RetryOnException;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.PageObjects.Web.Utility.panel;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.Tests.WebUI.EndToEndTests
{
    public class E2E002 : BaseEndToEndTest
    {
        private static String jsonSettings = excelReadedUtils.GetCellValue(1, 4, 5);
        private static Item settingsItem = JsonConvert.DeserializeObject<Item>(jsonSettings);
        private String code = settingsItem.code;

        [RetryOnException(ListOfExceptions = new[] { typeof(Exception) })]
        [Retry(numberOfTryFroWebTests)]
        [Test]
        public void E2E002_Test()
        {
            String name = settingsItem.name;
            String descripttion = settingsItem.description;
            String typeOfEvent = settingsItem.type;

            ManageSecuredWorkforcePage manageSecuredWorkforcePage = LoginIntoDefaultUtility(utilityUser, settingsItem.operatingCompany, "All Active Events");
            EventPage createEventPage = manageSecuredWorkforcePage.GetHeaderPanel()
                .OpenEventDropdownMenu()
                .ClickManageEvent()
                .clickAddButton();
            createEventPage.EnterCode(code);
            createEventPage.EnterName(name);
            createEventPage.SelectTypeOfEvent(typeOfEvent);
            createEventPage.EnterDescription(descripttion);
            createEventPage.SelectOperatingCompany(settingsItem.operatingCompany);
            createEventPage.checkDefaultEventCheckbox();
            createEventPage.SelectEventStartDate(settingsItem.eventStart);
            createEventPage.SelectEventEndDate(settingsItem.eventEnd);

            createEventPage.clickAddButton()
                .ClickConfirmButton();

            MessageNotificationPanel messageNotificationPanel = createEventPage.GetMessageNotificationPanel();
            createEventPage.GetAssertionUtils().EquialAssertion("Verify that Toast panel present with expected test", "The record has been added successfully. Click view to see the detail.", messageNotificationPanel.GetNotificationText());

            messageNotificationPanel.ClickOnPresentLink();
            EventsPage eventsPage = new EventsPage(GetDefaultBaseInformation());

            eventsPage.GetEventsPanel().GetAssertionUtils().TrueAssertion("Verify that Row with expected code present on any page", eventsPage.GetEventsPanel().IsRowWithCodePresentOnAnyPage(code));

            WRMAutotests.PageObjects.Web.Utility.panel.EventsPanel.Row fouldRow = eventsPage.GetEventsPanel().GetRowByCodeFromAnyPage(code);

            fouldRow.GetAssertionUtils().EquialAssertion("Verify that row have expected name", name, fouldRow.GetName());
            fouldRow.GetAssertionUtils().EquialAssertion("Verify that row have expected type", typeOfEvent, fouldRow.GetType());
            fouldRow.GetAssertionUtils().EquialAssertion("Verify that row have expected description", descripttion, fouldRow.GetDescription());
            fouldRow.GetAssertionUtils().EquialAssertion("Verify that row have expected status", "Active", fouldRow.GetStatus());
            fouldRow.GetAssertionUtils().EquialAssertion("Verify that row have expected updated value", "just now", fouldRow.GetUpdated());
            fouldRow.GetAssertionUtils().EquialAssertion("Verify that row have expected Email", utilityUser.GetEmail(), fouldRow.GetUpdatedBy());
            fouldRow.GetAssertionUtils().EquialAssertion("Verify that row have expected operation company", settingsItem.operatingCompany, fouldRow.GetOperatingCompany());

            //verify that created event present from dropdown menu on the Header panel
            eventsPage.GetHeaderPanel()
                .GetAssertionUtils()
                .TrueAssertion("Verify that created event present from dropdown menu", eventsPage.GetHeaderPanel().OpenEventDropdownMenu().GetEventNames().Contains(name));

            //verify that created event present on the 
            WRMAutotests.PageObjects.Web.Utility.panel.LoginPanel loginPanel = eventsPage.GetHeaderPanel()
                .OpenAccountDropdownMenu()
                .ClickLogoutButton()
                .ClickLoginButton()
                .GetLoginPanel();
            loginPanel.EnterEmail(utilityUser.GetEmail());
            loginPanel.EnterPassword(utilityUser.GetPassword());
            EventSelectionPage eventSelectionPage = loginPanel.ClickLoginButton();

            IList<string> eventNames = eventSelectionPage.GetWelcomeToStormManagerPanel().GetEvents().Select(s => s.Replace(" Active", "")).ToList();
            eventSelectionPage.GetWelcomeToStormManagerPanel().GetAssertionUtils().TrueAssertion("Verify that created event present on as option", eventNames.Contains(name));

        }

        [TearDown]
        public void RemoveCreatedEvent()
        {
            BaseInformation baseInformation = AddNewDriverWithDefaultSettings();
            ManageSecuredWorkforcePage manageSecuredWorkforcePage = LoginIntoDefaultUtility(baseInformation, utilityUser, settingsItem.operatingCompany, "All Active Events");
            manageSecuredWorkforcePage.GetHeaderPanel()
                .OpenEventDropdownMenu()
                .ClickManageEvent()
                .GetEventsPanel()
                .GetRowByCodeFromAnyPage(code)
                .ClickOperationButton()
                .ClickCancelEvent()
                .ClickConfirmButton();
            Thread.Sleep(15000);
        }

        private class Item
        {
            public string code;
            public string name;
            public string type;
            public string description;
            public string defaultEvent;
            public string operatingCompany;
            public DateTime eventStart;
            public DateTime eventEnd;
        }

    }
}
