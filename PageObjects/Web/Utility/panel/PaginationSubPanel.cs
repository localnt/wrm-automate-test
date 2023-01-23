using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.panel
{
    public class PaginationSubPanel : BasePageObject
    {

        [FindsBy(How = How.CssSelector, Using = "a.dxp-button.dxp-bi")]
        private IList<IWebElement> prevNextButtons;

        [FindsBy(How = How.XPath, Using = ".//a[contains(@onclick, 'PN0')]")]
        private IWebElement firstPageButton;

        public PaginationSubPanel(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new WRMAutotests.Utility.ReportUtils(baseInformation, "Pagination", "sub panel"))
        {


        }

        public Boolean IsNextButtonEnabled()
        {
            try
            {
                return prevNextButtons.Last().FindElement(By.CssSelector("img")).GetAttribute("alt").Equals("Next");
            }
            catch (System.InvalidOperationException ex)
            {
                return false;
            }

        }

        public void ClickNextButton()
        {
            GetReportUtils().ClickButton("Next");
            GetWebElementUtils().clickWebElement(prevNextButtons.Last());
            GetWaitUtils().WaitForLoadingPanelAbsent();
        }

        public void ClickFirstPage()
        {
            GetReportUtils().ClickButton("First page");
            if (GetWebElementUtils().IsWebElementPresent(firstPageButton))
            {
                GetWebElementUtils().clickWebElement(firstPageButton);
            }
            GetWaitUtils().WaitForLoadingPanelAbsent();
        }


    }
}
