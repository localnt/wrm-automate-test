using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.BaseElements;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.panel
{
    public class WelcomeToStormManagerPanel : BasePageObject
    {

        private static By rootLocator = By.CssSelector(".Main_Center");

        [FindsBy(How = How.CssSelector, Using = "div[title='Access without event selection'][class='dxbButton_DevEx dxbButtonSys dxbTSys']")]
        private IWebElement accessWithoutEventSelection;

        [FindsBy(How = How.XPath, Using = ".//div[@class='Main-float-left'][.//input[@value='— Select Operating Company —']]")]
        private IWebElement OperatingCompanyDropdownRootElement;

        [FindsBy(How = How.XPath, Using = ".//div[@class='Main-float-left'][.//input[contains(@name, 'gluEvents')]]")]
        private IWebElement eventSelectorRootElement;

        public WelcomeToStormManagerPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Welcome to Storm manager", "panel"))
        {
        }

        public ManageSecuredWorkforcePage ClickAccessWithoutEventSelection()
        {
            GetReportUtils().ClickButton("Access without event selection");
            Thread.Sleep(5000);
            GetWebElementUtils().clickWebElement(accessWithoutEventSelection);
            Thread.Sleep(3000);
            GetWaitUtils().WaitForElementInvisible(By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_ASPxLoadingPanel1"));
            Thread.Sleep(3000);
            return new ManageSecuredWorkforcePage(GetBaseInformation());
        }


        public void SelectOperatingCompany(String operationCompany)
        {
            new StandardDropdownMenu(GetBaseInformation(), OperatingCompanyDropdownRootElement, new WRMAutotests.Utility.ReportUtils(GetBaseInformation(), "Operating company", "dropdown menu")).SelectMenuElement(operationCompany);
        }

        public ManageSecuredWorkforcePage SelectEvent(String eventName)
        {
            new StandardDropdownMenu(GetBaseInformation(), eventSelectorRootElement, new WRMAutotests.Utility.ReportUtils(GetBaseInformation(), "Event", "Dropdown menu")).SelectMenuElement(eventName);
            return new ManageSecuredWorkforcePage(GetBaseInformation());
        }

        public IList<String> GetEvents()
        {
            return (new StandardDropdownMenu(GetBaseInformation(), eventSelectorRootElement, new WRMAutotests.Utility.ReportUtils(GetBaseInformation(), "Event", "Dropdown menu"))).GetOptionNames();
        }



    }
}
