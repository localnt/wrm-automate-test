using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Contractor.pages;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.panels
{
    public class ResourcesPoolPanel : BasePageObject
    {
        private static By rootLocator = By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_cbpIntenalCrewLayout_cpnMain_gvCrewStructure");

        [FindsBy(How = How.CssSelector, Using = "tr[class*='Row_DevEx']")]
        private IList<IWebElement> rows;

        public ResourcesPoolPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Resources pool table", "panel"))
        {
        }

        public IList<ResourcePoolRow> GetResourcePoolRows()
        {
            IList<ResourcePoolRow> result = new List<ResourcePoolRow>();
            foreach (var row in rows)
            {
                result.Add(new ResourcePoolRow(GetBaseInformation(), row));
            }
            return result;
        }

        public IList<ResourcePoolRow> GetResourcePoolRowsByResourcePoolName(String name)
        {
            return GetResourcePoolRows().Where(r => r.GetResourcePoolName().Equals(name)).ToList();
        }


        public class ResourcePoolRow : BasePageObject
        {

            [FindsBy(How = How.CssSelector, Using = "div[title='Edit the record']")]
            private IWebElement operationButton;

            [FindsBy(How = How.XPath, Using = "./td[2]/a")]
            private IWebElement recourcePoolLabel;

            [FindsBy(How = How.XPath, Using = "./td[3]")]
            private IWebElement resourcePoolNamelabel;

            [FindsBy(How = How.XPath, Using = "./td[5]")]
            private IWebElement sourceLocation;

            [FindsBy(How = How.XPath, Using = "./td[6]")]
            private IWebElement resource;

            [FindsBy(How = How.XPath, Using = "./td[7]")]
            private IWebElement crew;

            [FindsBy(How = How.XPath, Using = "./td[8]")]
            private IWebElement crewSize;

            [FindsBy(How = How.XPath, Using = "./td[9]")]
            private IWebElement bucket;

            [FindsBy(How = How.XPath, Using = "./td[10]")]
            private IWebElement digger;

            [FindsBy(How = How.XPath, Using = "./td[11]/span")]
            private IWebElement onIouCell;

            [FindsBy(How = How.XPath, Using = "./td[13]")]
            private IWebElement modified;

            public ResourcePoolRow(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Resource pool", "row"))
            {
            }

            public ResourcePoolEditPage ClickOperationButton()
            {
                GetReportUtils().ClickButton("Operation");
                GetWebElementUtils().clickWebElement(operationButton);
                return new ResourcePoolEditPage(GetBaseInformation());
            }

            public String GetResourcePool()
            {
                return recourcePoolLabel.Text;
            }

            public ResourcePoolOverviewPage ClickResourcePool()
            {
                GetReportUtils().ClickButton("Resource pool");
                GetWebElementUtils().clickWebElement(recourcePoolLabel);
                return new ResourcePoolOverviewPage(GetBaseInformation());
            }

            public String GetResourcePoolName()
            {
                return resourcePoolNamelabel.Text;
            }

            public String GetSourceLocation()
            {
                return sourceLocation.Text;
            }

            public String GetResource()
            {
                return resource.Text;
            }

            public String GetCrew()
            {
                return crew.Text;
            }

            public String GetCrewSize()
            {
                return crewSize.Text;
            }

            public String GetBucket()
            {
                return bucket.Text;
            }

            public String GetDigger()
            {
                return digger.Text;
            }

            public Boolean IsOnIouChecked()
            {
                return onIouCell.GetAttribute("class").Contains("edtCheckBoxChecked");
            }

            public String GetModified()
            {
                return modified.Text;
            }

        }

    }
}
