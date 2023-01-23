using Newtonsoft.Json;
using RetryOnException;
using WRMAutotests.PageObjects.Web.Contractor.pages;
using WRMAutotests.Utility.Web;
using static WRMAutotests.PageObjects.Web.Contractor.panels.ResourcesPoolPanel;

namespace WRMAutotests.Tests.WebUI.EndToEndTests
{
    public class E2E006 : BaseEndToEndTest
    {

        private static String operatingCompanyName = excelReadedUtils.GetCellValue(1, 8, 2).Equals("") ? defaultOperatingCompany : excelReadedUtils.GetCellValue(1, 8, 2);
        private static String eventName = excelReadedUtils.GetCellValue(1, 8, 3).Equals("") ? defaultEvent : excelReadedUtils.GetCellValue(1, 8, 3);
        private static String discipline = excelReadedUtils.GetCellValue(1, 8, 4).Equals("") ? defaultDiscipline : excelReadedUtils.GetCellValue(1, 8, 4);
        private InternalWorkforcePage.Tabs disciplineTab = InternalWorkforcePage.GetTabByNameFoTab(discipline);

        private static InputFormSettings settingsInputForm = JsonConvert.DeserializeObject<InputFormSettings>(excelReadedUtils.GetCellValue(1, 8, 5));
        private String resourcePoolName = settingsInputForm.ResourcePoolName + GetRandomValuesUtilities().GetRandomValue();


        [RetryOnException(ListOfExceptions = new[] { typeof(Exception) })]
        [Retry(numberOfTryFroWebTests)]
        [Test]
        public void E2E006_Test()
        {
            ;
            String sourceLocation = settingsInputForm.SourceLocation;
            int estimatedResources = int.Parse(settingsInputForm.EstimatedResources);
            int estimatedCrews = int.Parse(settingsInputForm.EstimatedCrews);
            int crewSize = int.Parse(settingsInputForm.CrewSize);
            int estimatedBuckets = int.Parse(settingsInputForm.EstimatedBuckets);
            int estimatedDiggers = int.Parse(settingsInputForm.EstimatedDiggers);
            bool IouCheckboxChecked = settingsInputForm.OnIouCheckbox.Equals("Checked") ? true : false;

            MainPage mainPage = LoginUntoDefaultContractor(contractorUser);
            InternalWorkforcePage internalWorkforcePage = CreateResourcePool(mainPage, disciplineTab, resourcePoolName, sourceLocation, estimatedResources, estimatedCrews, crewSize, estimatedBuckets, estimatedDiggers, IouCheckboxChecked, operatingCompanyName);

            ResourcePoolRow resourcePoolRow = internalWorkforcePage.GetResourcesPoolPanel().GetResourcePoolRowsByResourcePoolName(resourcePoolName)[0];

            resourcePoolRow.GetAssertionUtils().EquialAssertion("Verify that Source location expected: " + sourceLocation, resourcePoolRow.GetSourceLocation(), sourceLocation);
            resourcePoolRow.GetAssertionUtils().EquialAssertion("Verify that Estimated Resource location expected: " + estimatedResources.ToString(), resourcePoolRow.GetResource(), estimatedResources.ToString());
            resourcePoolRow.GetAssertionUtils().EquialAssertion("Verify that Estimated Crew expected: " + estimatedCrews.ToString(), resourcePoolRow.GetCrew(), estimatedCrews.ToString());
            resourcePoolRow.GetAssertionUtils().EquialAssertion("Verify that Crew Size expected: " + crewSize.ToString(), resourcePoolRow.GetCrewSize(), crewSize.ToString());
            resourcePoolRow.GetAssertionUtils().EquialAssertion("Verify that Estimated buckets expected: " + estimatedBuckets.ToString(), resourcePoolRow.GetBucket(), estimatedBuckets.ToString());
            resourcePoolRow.GetAssertionUtils().EquialAssertion("Verify that Estimated diggers expected: " + estimatedDiggers.ToString(), resourcePoolRow.GetDigger(), estimatedDiggers.ToString());
            resourcePoolRow.GetAssertionUtils().EquialAssertion("Verify that Modified date expected: " + "just now", resourcePoolRow.GetModified(), "just now");
            if (IouCheckboxChecked)
            {
                resourcePoolRow.GetAssertionUtils().TrueAssertion("Verify that IOU checked", resourcePoolRow.IsOnIouChecked());
            }
            else
            {
                resourcePoolRow.GetAssertionUtils().TrueAssertion("Verify that IOU unchecked", !resourcePoolRow.IsOnIouChecked());
            }


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
                .ClickTab(disciplineTab)
                .GetResourcesPoolPanel()
                .GetResourcePoolRowsByResourcePoolName(resourcePoolName)[0]
                .ClickOperationButton()
                .ClickDeleteButton()
                .ClickConfirmButton();
            Thread.Sleep(30000);
        }

        private class InputFormSettings
        {
            public string ResourcePoolName;
            public string SourceLocation;
            public string OnIouCheckbox;
            public string EstimatedResources;
            public string EstimatedCrews;
            public string CrewSize;
            public string EstimatedBuckets;
            public string EstimatedDiggers;
        }

    }
}
