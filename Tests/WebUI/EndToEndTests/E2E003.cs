using MimeKit;
using Newtonsoft.Json;
using RetryOnException;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.Utility;

namespace WRMAutotests.Tests.WebUI.EndToEndTests
{
    public class E2E003 : BaseEndToEndTest
    {

        private String operatingCompanyName = excelReadedUtils.GetCellValue(1, 5, 2).Equals("") ? defaultOperatingCompany : excelReadedUtils.GetCellValue(1, 5, 2);
        private String eventName = excelReadedUtils.GetCellValue(1, 5, 3).Equals("") ? defaultEvent : excelReadedUtils.GetCellValue(1, 5, 3);
        private String discipline = excelReadedUtils.GetCellValue(1, 5, 4).Equals("") ? defaultDiscipline : excelReadedUtils.GetCellValue(1, 5, 4);

        private static InputFormSettings settingsInputForm = JsonConvert.DeserializeObject<InputFormSettings>(excelReadedUtils.GetCellValue(1, 5, 5));
        private static ContinueParameterSettings settingsContinueParameter = JsonConvert.DeserializeObject<ContinueParameterSettings>(excelReadedUtils.GetCellValue(1, 5, 6));

        [RetryOnException(ListOfExceptions = new[] { typeof(Exception) })]
        [Retry(numberOfTryFroWebTests)]
        [Test]
        public void E2E003_Test()
        {
            String contractor = settingsContinueParameter.contractorName;
            String subject = settingsInputForm.subject + GetRandomValuesUtilities().GetRandomValue();
            String message = settingsInputForm.massage + GetRandomValuesUtilities().GetRandomValue();
            message = message.Replace("\n", "").Replace("\t", "").Trim();

            ManageSecuredWorkforcePage manageSecuredWorkforcePage = LoginIntoDefaultUtility(utilityUser, operatingCompanyName, eventName);
            CreateCrewAvailabilityRequest(manageSecuredWorkforcePage, operatingCompanyName, contractor, subject, message, discipline);

            Thread.Sleep(60000);
            MailRepository mailRepository = new MailRepository(contractorUser);
            IList<MimeMessage> foundEmails = mailRepository.GetUnreadEmailsByPartOfSubjectAndReciveEmails(subject, contractorUser.GetEmail());

            manageSecuredWorkforcePage.GetAssertionUtils().TrueAssertionWithoutNameOfPageObject("Verify that Expected Email present in the Inbox", foundEmails.Count > 0);
            manageSecuredWorkforcePage.GetAssertionUtils().TrueAssertionWithoutNameOfPageObject("Verify that Expected Email letter contain expected message: " + message, foundEmails[0].HtmlBody.Contains(message));

        }


        private class InputFormSettings
        {
            public string subject;
            public string massage;
            public string Utility;
        }

        private class ContinueParameterSettings
        {
            public string contractorName;
        }

    }
}
