using MimeKit;
using Newtonsoft.Json;
using RetryOnException;
using WRMAutotests.PageObjects.Web.Contractor.pages;
using WRMAutotests.PageObjects.Web.Contractor.panels;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.Tests.WebUI.EndToEndTests
{
    public class E2E005 : BaseEndToEndTest
    {

        private String operatingCompanyName = excelReadedUtils.GetCellValue(1, 7, 2).Equals("") ? defaultOperatingCompany : excelReadedUtils.GetCellValue(1, 7, 2);
        private String eventName = excelReadedUtils.GetCellValue(1, 7, 3).Equals("") ? defaultEvent : excelReadedUtils.GetCellValue(1, 7, 3);
        private String discipline = excelReadedUtils.GetCellValue(1, 7, 4).Equals("") ? defaultDiscipline : excelReadedUtils.GetCellValue(1, 7, 4);

        private String subject = GetRandomValuesUtilities().GetRandomValue();
        private String message = GetRandomValuesUtilities().GetRandomValue();
        private String contractor = JsonConvert.DeserializeObject<ContinueParameterSettings>(excelReadedUtils.GetCellValue(1, 7, 6)).contractorName;
        [SetUp]
        public void createCrewAvailabilityRequest()
        {
            BaseInformation baseInformation = AddNewDriverWithDefaultSettings();
            ManageSecuredWorkforcePage manageSecuredWorkforcePage = LoginIntoDefaultUtility(baseInformation, utilityUser, operatingCompanyName, eventName);
            CreateCrewAvailabilityRequest(manageSecuredWorkforcePage, operatingCompanyName, contractor, subject, message, discipline);
            RemoveDriver(baseInformation);
        }


        [RetryOnException(ListOfExceptions = new[] { typeof(Exception) })]
        [Retry(numberOfTryFroWebTests)]
        [Test]
        public void E2E005_Test()
        {
            MainPage mainPage = LoginUntoDefaultContractor(contractorUser);
            MessagePanel messagePanel = mainPage.GetHeaderPanel()
                .ClickMessagesButoon()
                .GetMessageCenterPanel()
                .GetEmailRowsByPartSubject(subject)[0]
                .ClickSubject();

            messagePanel.GetAssertionUtils().TrueAssertion("Verify that Message contain expected text: " + message, messagePanel.GetMessageText().Contains(message));

            //check emails
            Thread.Sleep(60000);
            MailRepository mailRepository = new MailRepository(contractorUser);
            IList<MimeMessage> foundEmails = mailRepository.GetUnreadEmailsByPartOfSubjectAndReciveEmails(subject, contractorUser.GetEmail());
            messagePanel.GetAssertionUtils().TrueAssertionWithoutNameOfPageObject("Verify that Expected Email present in the Inbox", foundEmails.Count > 0);
            messagePanel.GetAssertionUtils().TrueAssertionWithoutNameOfPageObject("Verify that Expected Email letter contain expected message: " + message, foundEmails[0].HtmlBody.Contains(message));
        }

        private class ContinueParameterSettings
        {
            public string contractorName;
        }

    }
}
