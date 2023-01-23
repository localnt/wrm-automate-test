using Allure.Commons;
using WRMAutotests.Utility;

namespace WRMAutotests.Tests
{
    abstract public class BaseTest
    {

        //Get Settings from ConfigDemo table
        public static ExcelReadedUtils excelReadedUtils = new ExcelReadedUtils("configDemo1.4.xlsx");
        private Boolean clearPreviousAllureResults = excelReadedUtils.GetCellValue(0, 9, 1).Equals("1") ? true : Boolean.Parse(excelReadedUtils.GetCellValue(0, 9, 1));
        public Boolean makeScreensootForEveryReportStep = Boolean.Parse(excelReadedUtils.GetCellValue(0, 16, 1));

        public const int numberOfTryFroWebTests = 1;//when 1 - just 1 run, so no make sense
        public int timeZoneDifferenceHours = 7;

        public static RandomValuesUtilities GetRandomValuesUtilities()
        {
            String modeOfNameGeneration = excelReadedUtils.GetCellValue(0, 10, 1);
            if (modeOfNameGeneration.Equals("Field"))
            {
                String newBaseRandomString = excelReadedUtils.GetCellValue(0, 10, 2).Trim().Replace(" ", "");
                return new RandomValuesUtilities(newBaseRandomString);
            }
            else
            {
                return new RandomValuesUtilities();
            }
        }

        [OneTimeSetUp]
        public void ClearResultsDir()
        {
            if (clearPreviousAllureResults)
            {
                AllureLifecycle.Instance.CleanupResultDirectory();
            }

        }


    }
}
