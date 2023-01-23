using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Base
{
    public abstract class BasePageObject
    {

        private BaseInformation baseInformation;
        private WaitUtils waitUtils;
        private WebElementUtils webElementUtils;
        private ReportUtils reportUtils;
        private AssertionUtils assertionUtils;

        public BasePageObject(BaseInformation baseInformation, ReportUtils reportUtils)
        {
            this.baseInformation = baseInformation;
            PageFactory.InitElements(this.baseInformation.GetDriver(), this);
            this.reportUtils = reportUtils;
        }

        public BasePageObject(BaseInformation baseInformation, By rootLocator, ReportUtils reportUtils)
        {
            this.baseInformation = baseInformation;
            PageFactory.InitElements(this.baseInformation.GetDriver().FindElement(rootLocator), this);
            this.reportUtils = reportUtils;
        }

        public BasePageObject(BaseInformation baseInformation, IWebElement rootElement, ReportUtils reportUtils)
        {
            this.baseInformation = baseInformation;
            PageFactory.InitElements(rootElement, this);
            this.reportUtils = reportUtils;
        }

        public BaseInformation GetBaseInformation()
        {
            return this.baseInformation;
        }

        public WaitUtils GetWaitUtils()
        {
            if (waitUtils == null)
            {
                waitUtils = new WaitUtils(GetBaseInformation().GetDriver());
            }
            return waitUtils;
        }

        public WebElementUtils GetWebElementUtils()
        {
            if (webElementUtils == null)
            {
                webElementUtils = new WebElementUtils(GetBaseInformation().GetDriver(), GetWaitUtils());
            }
            return webElementUtils;
        }

        public ReportUtils GetReportUtils()
        {
            return reportUtils;
        }

        public AssertionUtils GetAssertionUtils()
        {
            if (assertionUtils == null)
            {
                this.assertionUtils = new AssertionUtils(GetReportUtils());
            }
            return assertionUtils;
        }




    }
}
