using Allure.Commons;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.Utility
{
    public class ReportUtils
    {
        private BaseInformation baseInformation;
        private String nameOfPageObject;
        private String typeOfPageObject;
        public Boolean isStepsEnabled = true;

        public ReportUtils(BaseInformation baseInformation, String nameOfPageObject, String typeOfPageObject)
        {
            this.baseInformation = baseInformation;
            this.nameOfPageObject = nameOfPageObject;
            this.typeOfPageObject = typeOfPageObject;
        }


        public void ClickButton(String nameOfButton)
        {
            String step = String.Format("Click on the \"{0}\" button", nameOfButton);
            AllureStepWithPageObject(step);
        }

        public void EnterValueToField(String nameOfField, String value)
        {
            String step = String.Format("Enter \"{0}\" into \"{1}\" field", value, nameOfField);
            AllureStepWithPageObject(step);
        }

        public void AllureStepWithPageObject(String step)
        {
            AllureStep(String.Format("{0} on the \"{1}\" {2}", step, nameOfPageObject, typeOfPageObject));
        }

        public void AllureStep(String step)
        {
            String time = DateTime.Now.ToString("HH:mm:ss");
            if (isStepsEnabled)
            {
                BaseAllureStep(String.Format("[{0}] [Session Id: {1}] {2}", time, baseInformation.GetSessionId(), step));
            }

        }


        [AllureStep("{0}")]
        private void BaseAllureStep(String step)
        {
            if (baseInformation.makeScreenshootEveryStep())
            {
                MakeScreenshoot(baseInformation);
            }
        }

        static public void MakeScreenshoot(BaseInformation baseInformation)
        {
            var screenshot = ((ITakesScreenshot)baseInformation.GetDriver()).GetScreenshot();
            var filename = TestContext.CurrentContext.Test.MethodName + "_screenshot_" + DateTime.Now.Ticks + ".png";
            screenshot.SaveAsFile(filename, ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(filename);
            AllureLifecycle.Instance.AddAttachment(filename, "image/png", filename);
        }


    }
}
