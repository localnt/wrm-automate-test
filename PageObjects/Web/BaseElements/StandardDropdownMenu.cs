using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.BaseElements
{
    public class StandardDropdownMenu : BasePageObject
    {

        [FindsBy(How = How.CssSelector, Using = "td[onmousedown*='DDDropDown']")]
        private IWebElement dropdownButton;

        [FindsBy(How = How.CssSelector, Using = "table > tbody > tr[class*='DevEx']")]
        private IList<IWebElement> dropdownMenuItems;


        public StandardDropdownMenu(BaseInformation baseInformation, IWebElement rootElement, WRMAutotests.Utility.ReportUtils reportUtils) : base(baseInformation, rootElement, reportUtils)
        {

        }

        public IList<String> GetOptionNames()
        {
            ClickOpenDropdownButton();
            IList<String> result = new List<String>();
            foreach (IWebElement menuItem in dropdownMenuItems)
            {
                result.Add(menuItem.FindElement(By.CssSelector("tr > td")).Text);
            }


            ClickOpenDropdownButton();
            return result;
        }


        public void ClickOpenDropdownButton()
        {
            GetReportUtils().ClickButton("Open dropdown");
            Thread.Sleep(2000);
            GetWebElementUtils().clickWebElement(dropdownButton);
            Thread.Sleep(2000);
        }

        public void SelectMenuElement(String menuElement)
        {
            ClickOpenDropdownButton();
            GetReportUtils().ClickButton(menuElement + " menu element");
            IWebElement targetOption = dropdownMenuItems.First(e => e.FindElement(By.CssSelector("tr > td")).Text.Contains(menuElement));
            Thread.Sleep(2000);
            GetWebElementUtils().clickWebElement(targetOption);
            Thread.Sleep(2000);
        }

    }
}
