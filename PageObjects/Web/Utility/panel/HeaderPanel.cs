using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Utility.pages;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.panel
{
    public class HeaderPanel : BasePageObject
    {

        private static By rootLocator = By.CssSelector("div#header");

        [FindsBy(How = How.CssSelector, Using = "#TopPanel_gluEvents_B-1")]
        private IWebElement openEventDropdownMenuButton;

        [FindsBy(How = How.CssSelector, Using = "#TopPanel_upnEventsDropdown")]
        private IWebElement eventDropdownRootElement;

        [FindsBy(How = How.CssSelector, Using = "td[id*='OCselection']")]
        private IWebElement openOperatingCompanyDropDownMenuButton;

        [FindsBy(How = How.CssSelector, Using = "#TopPanel_UpdatePanel1")]
        private IWebElement operatingCompanyRootElement;

        [FindsBy(How = How.CssSelector, Using = "#TopPanel_ASPxMenu1_DXI2_")]
        private IWebElement workforceButton;

        [FindsBy(How = How.CssSelector, Using = "#divMasterDisplayName")]
        private IWebElement accountRootElement;

        [FindsBy(How = How.CssSelector, Using = "div#divMessageCenterBadgeIcon")]
        private IWebElement messageButton;

        public HeaderPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Utility Header", "panel"))
        {
        }

        public void ClickOpenEventsDropdownMenu()
        {
            GetReportUtils().ClickButton("Open events dropdown menu");
            GetWebElementUtils().clickWebElement(openEventDropdownMenuButton);
        }

        public WorkforceMenuPanel OpenWorkforceEventsManuPanel()
        {
            GetReportUtils().AllureStepWithPageObject("Open Workforce menu");
            GetWaitUtils().WaitForElementClicable(workforceButton);
            Thread.Sleep(5000);
            GetWebElementUtils().clickWebElement(workforceButton);
            Thread.Sleep(5000);
            return new WorkforceMenuPanel(GetBaseInformation());
        }

        public void ClickOpenOperatingCompanyDropdownMenuButton()
        {
            GetReportUtils().ClickButton("Open Operating Company dropdown menu");
            GetWebElementUtils().clickWebElement(openOperatingCompanyDropDownMenuButton);
        }

        public OperatingCompanyMenu OpenOperatingCompanyDropdownMenu()
        {
            ClickOpenOperatingCompanyDropdownMenuButton();
            Thread.Sleep(2000);
            return new OperatingCompanyMenu(GetBaseInformation(), operatingCompanyRootElement);
        }

        public EventsDropdownMenu OpenEventDropdownMenu()
        {
            ClickOpenEventsDropdownMenu();
            Thread.Sleep(4000);
            return new EventsDropdownMenu(GetBaseInformation(), eventDropdownRootElement);
        }

        public AccountDropdownMenu OpenAccountDropdownMenu()
        {
            GetReportUtils().ClickButton("Account Dropdown menu");
            GetWebElementUtils().clickWebElement(accountRootElement);
            return new AccountDropdownMenu(GetBaseInformation(), accountRootElement);
        }

        public MessageCenterPage ClickMessagesButoon()
        {
            GetReportUtils().ClickButton("Messages");
            GetWebElementUtils().clickWebElement(messageButton);
            Thread.Sleep(30000);
            return new MessageCenterPage(GetBaseInformation());
        }

        public static Boolean IsHeaderPanelPresent(BaseInformation baseInformation)
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i != 0)
                    {
                        Thread.Sleep(2000);
                    }
                    baseInformation.GetDriver().FindElement(rootLocator);
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public class EventsDropdownMenu : BasePageObject
        {

            [FindsBy(How = How.CssSelector, Using = "tr[id*='BtnManagementEvent']")]
            private IWebElement manageEventsButton;

            [FindsBy(How = How.CssSelector, Using = "tr[id*='TopPanel_gluEvents_DDD'] .dxgv")]
            private IList<IWebElement> eventRoots;

            public EventsDropdownMenu(BaseInformation baseInformation, IWebElement rootElement) :
                base(baseInformation, rootElement, new WRMAutotests.Utility.ReportUtils(baseInformation, " Header Events", "dropdown menu"))
            {

            }

            public IList<String> GetEventNames()
            {
                IList<String> result = new List<String>();
                foreach (IWebElement element in eventRoots)
                {
                    result.Add(element.FindElement(By.CssSelector("td.EventDropdownColEventName,.EventDropdownColAllEvent")).Text);
                }
                return result;
            }

            public void ClickEventByName(String name)
            {
                name = name.Trim();
                GetReportUtils().ClickButton("Event name: " + name);
                Thread.Sleep(5000);
                foreach (IWebElement element in eventRoots)
                {
                    if (element.FindElement(By.CssSelector("td.EventDropdownColEventName,.EventDropdownColAllEvent")).Text.Trim().Equals(name))
                    {
                        GetWebElementUtils().clickWebElement(element);
                        Thread.Sleep(30000);
                        return;
                    }
                }

            }

            public EventsPage ClickManageEvent()
            {
                GetReportUtils().ClickButton("Manage");
                GetWebElementUtils().clickWebElement(manageEventsButton);
                return new EventsPage(GetBaseInformation());
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

            public DefaultPage ClickLogoutButton()
            {
                GetReportUtils().ClickButton("Logout");
                GetWebElementUtils().clickWebElement(logoutButton);
                return new DefaultPage(GetBaseInformation());
            }

        }

        public class OperatingCompanyMenu : BasePageObject
        {

            [FindsBy(How = How.CssSelector, Using = "#TopPanel_cbOCselection_DDD_L_LBT tbody > tr > td")]
            IList<IWebElement> menuElements;

            public OperatingCompanyMenu(BaseInformation baseInformation, IWebElement rootElement)
                : base(baseInformation, rootElement, new WRMAutotests.Utility.ReportUtils(baseInformation, "Operating company", "menu"))
            {
            }

            public void ClickOperatingCompanyByName(String nameOfOperationCompany)
            {
                GetReportUtils().ClickButton("Operating company: " + nameOfOperationCompany);
                foreach (IWebElement element in menuElements)
                {
                    if (element.Text.Equals(nameOfOperationCompany))
                    {
                        GetWebElementUtils().clickWebElement(element);
                        Thread.Sleep(25000);
                        return;
                    }

                }
                throw new AssertionException("Absent Operating company with name: " + nameOfOperationCompany);
            }

        }

        public class WorkforceMenuPanel : BasePageObject
        {

            private static By rootLocator = By.CssSelector("table[id *='SiteMapControlWorkforce']");

            [FindsBy(How = How.CssSelector, Using = "td > a")]
            private IList<IWebElement> links;

            public WorkforceMenuPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new WRMAutotests.Utility.ReportUtils(baseInformation, "Workforce menu", "panel"))
            {
            }

            public ManageSecuredWorkforcePage ClickManageSecuredWorkforce()
            {
                ClickLinkByName("Manage Secured Workforce");
                return new ManageSecuredWorkforcePage(GetBaseInformation());
            }

            public CreawAvailabilityRequestPage ClickCrewAvailabilityRequestButton()
            {
                ClickLinkByName("Crew Availability Request");
                return new CreawAvailabilityRequestPage(GetBaseInformation());
            }

            public NonIouMarketplacePage ClickNonIouMarketplaceButton()
            {
                ClickLinkByName("Non-IOU Marketplace");
                Thread.Sleep(30000);
                return new NonIouMarketplacePage(GetBaseInformation());
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

    }
}
