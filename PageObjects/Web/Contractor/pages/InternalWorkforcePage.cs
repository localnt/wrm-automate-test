using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Contractor.panels;
using WRMAutotests.PageObjects.Web.Contractor.windows.crewavailabilitywindow;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.pages
{
    public class InternalWorkforcePage : BaseLoggedPage
    {

        [FindsBy(How = How.CssSelector, Using = "div[title='Add a new record']")]
        private IWebElement addbutton;

        [FindsBy(How = How.CssSelector, Using = "div[title='Crew Availability Form']")]
        private IWebElement crewAvailabilityFormButton;

        [FindsBy(How = How.CssSelector, Using = "ul.dxtc-strip > li.dxtc-tab")]
        private IList<IWebElement> tabs;

        [FindsBy(How = How.CssSelector, Using = "ul.dxtc-strip > li.dxtc-activeTab")]
        private IList<IWebElement> activeTabs;

        public InternalWorkforcePage(BaseInformation baseInformation) : base(baseInformation, new ReportUtils(baseInformation, "Internal workforce", "page"))
        {
        }

        public AddResourcePoolPage ClickAddButton()
        {
            GetReportUtils().ClickButton("Add");
            try
            {
                GetWebElementUtils().clickWebElement(addbutton);
            }
            catch (OpenQA.Selenium.ElementClickInterceptedException ex)
            {
                Thread.Sleep(5000);
                GetWebElementUtils().clickWebElement(addbutton);
            }
            catch (OpenQA.Selenium.StaleElementReferenceException ex)
            {
                Thread.Sleep(5000);
                GetWebElementUtils().clickWebElement(addbutton);
            }

            Thread.Sleep(7000);
            return new AddResourcePoolPage(GetBaseInformation());
        }

        public CrewAvailabilityFormWindow ClickCrewAvailabilityFormButton()
        {
            GetReportUtils().ClickButton("Crew Availability Form");
            GetWebElementUtils().clickWebElement(crewAvailabilityFormButton);
            Thread.Sleep(5000);
            GetWaitUtils().WaitForElementInvisible(By.CssSelector("##ASPxPanel2_ContentPlaceHolder1_ASPxLoadingPanel1"));
            Thread.Sleep(30000);
            return new CrewAvailabilityFormWindow(GetBaseInformation());
        }

        public ResourcesPoolPanel GetResourcesPoolPanel()
        {
            Thread.Sleep(10000);
            return new ResourcesPoolPanel(GetBaseInformation());
        }

        public InternalWorkforcePage ClickTab(Tabs tab)
        {
            Thread.Sleep(10000);
            switch (tab)
            {
                case Tabs.Airboats:
                    {
                        ClickTabByName("Airboats");
                        break;
                    }
                case Tabs.Damage_Assessment:
                    {
                        ClickTabByName("Damage Assessment");
                        break;
                    }
                case Tabs.Distribution_Line:
                    {
                        ClickTabByName("Distribution Line");
                        break;
                    }
                case Tabs.Distribution_Veg_Mgmt:
                    {
                        ClickTabByName("Distribution Veg Mgmt");
                        break;
                    }
                case Tabs.Substation:
                    {
                        ClickTabByName("Substation");
                        break;
                    }
                case Tabs.Support:
                    {
                        ClickTabByName("Support");
                        break;
                    }
                case Tabs.Transmission_Line:
                    {
                        ClickTabByName("Transmission Line");
                        break;
                    }
                case Tabs.UAS:
                    {
                        ClickTabByName("UAS");
                        break;
                    }
                case Tabs.UG_Network:
                    {
                        ClickTabByName("UG Network");
                        break;
                    }
            }
            return this;
        }

        private void ClickTabByName(String tabName)
        {
            GetReportUtils().ClickButton("Tab: " + tabName);
            IWebElement tabButton = null;
            try
            {
                tabButton = tabs.Where(r => r.FindElement(By.CssSelector("a > span")).Text.Equals(tabName)).First();
            }
            catch (Exception ex)
            {
                //checked that we already on expected tab or not
                if (activeTabs.Where(r => r.FindElement(By.CssSelector("a > span")).Text.Equals(tabName)).ToList().Count > 0)
                {
                    return;
                }
                else
                {
                    throw new AssertionException("Absent tab with name: " + tabName);
                }

            }

            GetWebElementUtils().clickWebElement(tabButton);
            GetWaitUtils().WaitForLoadingPanelAbsent();
            Thread.Sleep(10000);
            GetWaitUtils().WaitForLoadingPanelAbsent();
        }


        public enum Tabs
        {
            Airboats,
            Damage_Assessment,
            Distribution_Line,
            Distribution_Veg_Mgmt,
            Substation,
            Support,
            Transmission_Line,
            UAS,
            UG_Network
        }



        public static Tabs GetTabByNameFoTab(String nameOfTab)
        {

            switch (nameOfTab)
            {
                case "Airboats":
                    return Tabs.Airboats;
                case "Damage Assessment":
                    return Tabs.Damage_Assessment;
                case "Distribution Line":
                    return Tabs.Distribution_Line;
                case "Distribution Veg Mgmt":
                    return Tabs.Distribution_Veg_Mgmt;
                case "Substation":
                    return Tabs.Substation;
                case "Support":
                    return Tabs.Support;
                case "Transmission Line":
                    return Tabs.Transmission_Line;
                case "UAS":
                    return Tabs.UAS;
                case "UG Network":
                    return Tabs.UG_Network;
                default:
                    throw new AssertionException("Abasent Tab for: " + nameOfTab);
            }

        }

    }

}
