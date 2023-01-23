using WRMAutotests.Tests.WebUI.Base;

namespace WRMAutotests.Tests.WebUI.EndToEndTests
{
    public class BaseEndToEndTest : BaseWebTest
    {
        public static String defaultDiscipline = excelReadedUtils.GetCellValue(0, 12, 1);
        public static String defaultOperatingCompany = excelReadedUtils.GetCellValue(0, 14, 1);
        public static String defaultEvent = excelReadedUtils.GetCellValue(0, 15, 1);
        public static String defaultSourceLocation = excelReadedUtils.GetCellValue(0, 17, 1);
        public static Boolean checkIouCheckbox = Boolean.Parse(excelReadedUtils.GetCellValue(0, 11, 1));

    }
}
