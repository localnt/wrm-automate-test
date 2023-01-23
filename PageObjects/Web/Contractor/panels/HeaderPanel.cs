using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Contractor.pages;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.panels
{
    public class HeaderPanel : BasePageObject
    {

        private static OpenQA.Selenium.By rootLocator = By.CssSelector("div#header");

        [FindsBy(How = How.CssSelector, Using = "div#divMessageCenterBadgeIcon")]
        private IWebElement messageButton;

        [FindsBy(How = How.CssSelector, Using = "#TopPanel_ASPxMenu1_DXI2_")]
        private IWebElement workforceButton;

        [FindsBy(How = How.CssSelector, Using = "li#TopPanel_ASPxMenu1_DXI0_")]
        private IWebElement homeButton;

        [FindsBy(How = How.CssSelector, Using = "#divMasterDisplayName")]
        private IWebElement accountRootElement;

        public HeaderPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Contractor Header", "panel"))
        {

        }

        public MessageCenterPage ClickMessagesButoon()
        {
            GetReportUtils().ClickButton("Messages");
            GetWebElementUtils().clickWebElement(messageButton);
            return new MessageCenterPage(GetBaseInformation());
        }

        public WorkforceMenuPanel OpenWorkforceMenuPanel()
        {
            GetReportUtils().ClickButton("Workforce");
            GetWebElementUtils().clickWebElement(workforceButton);
            Thread.Sleep(3000);
            return new WorkforceMenuPanel(GetBaseInformation());
        }

        public static Boolean IsHeaderPanelPresent(BaseInformation baseInformation)
        {
            try
            {
                baseInformation.GetDriver().FindElement(rootLocator);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public MainPage ClickHomeButton()
        {
            GetReportUtils().ClickButton("Home");
            GetWaitUtils().WaitForLoadingPanelAbsent();
            Thread.Sleep(3000);
            GetWebElementUtils().clickWebElement(homeButton);
            //wait section for picture from main page
            Thread.Sleep(3000);
            GetWaitUtils().WaitForElementPresentByLocator(By.CssSelector("div.container.Main_Container.text-danger"));
            Thread.Sleep(3000);

            return new MainPage(GetBaseInformation());
        }

        public AccountDropdownMenu OpenAccountDropdownMenu()
        {
            GetReportUtils().ClickButton("Account Dropdown menu");
            GetWebElementUtils().clickWebElement(accountRootElement);
            return new AccountDropdownMenu(GetBaseInformation(), accountRootElement);
        }

        public class WorkforceMenuPanel : BasePageObject
        {

            private static By rootLocator = By.CssSelector("table[id *='SiteMapControlWorkforce']");

            [FindsBy(How = How.CssSelector, Using = "td > a")]
            private IList<IWebElement> links;

            public WorkforceMenuPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Workforce menu", "panel"))
            {
            }

            public ProcurementRequestPage ClickProcurementRequest()
            {
                String menuElement = "Procurement Request";
                GetReportUtils().ClickButton(menuElement);
                ClickLinkByName(menuElement);
                Thread.Sleep(10000);
                return new ProcurementRequestPage(GetBaseInformation());
            }

            public InternalWorkforcePage ClickInternalWorkforceButton()
            {
                String menuElement = "Internal Workforce";
                GetReportUtils().ClickButton(menuElement);
                ClickLinkByName(menuElement);
                Thread.Sleep(10000);
                return new InternalWorkforcePage(GetBaseInformation());
            }


            private void ClickLinkByName(String text)
            {
                GetReportUtils().AllureStepWithPageObject("Click on the link " + text);
                foreach (IWebElement link in links)
                {
                    if (link.Text.Equals(text))
                    {
                        GetWebElementUtils().clickWebElement(link);
                        return;
                    }
                }
                throw new AssertionException("Absent menu element for Workflow menu: " + text);

            }



        }

        public class AccountDropdownMenu : BasePageObject
        {

            [FindsBy(How = How.CssSelector, Using = "#TopPanel_Account_ASPxMenu_DXI0i1_")]
            private IWebElement logoutButton;

            public AccountDropdownMenu(BaseInformation baseInformation, IWebElement rootElement)
                : base(baseInformation, rootElement, new WRMAutotests.Utility.ReportUtils(baseInformation, "Account", "dropdown menu"))
            {

            }

            public void ClickLogoutButton()
            {
                GetReportUtils().ClickButton("Logout");
                GetWebElementUtils().clickWebElement(logoutButton);
            }

        }

    }
}
