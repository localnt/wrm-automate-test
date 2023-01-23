using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.Contractor.windows;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.panels
{
    public class ResourcePoolEditOrganizationPanel : BasePageObject
    {

        private static By rootLocator = By.CssSelector("table#ASPxPanel2_ContentPlaceHolder1_content_Table");

        [FindsBy(How = How.CssSelector, Using = "li#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_T0")]
        private IWebElement resourceTab;

        [FindsBy(How = How.CssSelector, Using = "li#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_T1")]
        private IWebElement equipmentTab;

        [FindsBy(How = How.CssSelector, Using = "a#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbResourceTab_btnAddResource")]
        private IWebElement addResourceButton;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbResourceTab_bkt_org_Resource_ASPxTextBox_I")]
        private IWebElement searchResourceField;

        [FindsBy(How = How.CssSelector, Using = "table#tb_resource tr")]
        private IList<IWebElement> resourceRootRows;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbResourceTab_bkt_org_Resource_ASPxButton")]
        private IWebElement searchResourceButton;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbEquipmentTab_btnAddEquipment")]
        private IWebElement addEquimentButton;

        [FindsBy(How = How.CssSelector, Using = "table#tb_equipment > tbody > tr")]
        private IList<IWebElement> equipmentRows;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnBucketOrg_upTree table[id='example-basic'] > tbody > tr")]
        private IList<IWebElement> organizationRows;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbEquipmentTab_bkt_org_Equipment_ASPxTextBox_I")]
        private IWebElement searchEquipmentField;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbEquipmentTab_bkt_org_Equipment_ASPxButton")]
        private IWebElement searchEquipmentButton;

        [FindsBy(How = How.CssSelector, Using = "div[title='Save the changes']")]
        private IWebElement saveButton;

        public ResourcePoolEditOrganizationPanel(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Resource Pool - Edit Organization", "panel"))
        {
        }

        public void EnterSeacrhEqupmentText(String equpment)
        {
            GetReportUtils().EnterValueToField("Search Equpment", equpment);
            searchEquipmentField.SendKeys(equpment);
            Thread.Sleep(3000);
        }

        public void ClickSearchEqupmentButton()
        {
            GetReportUtils().ClickButton("Search Equpment button");
            GetWebElementUtils().clickWebElement(searchEquipmentButton);
            Thread.Sleep(6000);
        }

        public void SearchEqupment(String equpment)
        {
            EnterSeacrhEqupmentText(equpment);
            ClickSearchEqupmentButton();
        }

        public void ClickResourceTab()
        {
            GetReportUtils().ClickButton("Resource tab");
            GetWebElementUtils().clickWebElement(resourceTab);
        }

        public void ClickEquimentTab()
        {
            GetReportUtils().ClickButton("Equipment tab");
            equipmentTab.Click();
        }

        public AddResourceWindow ClickAddResourceButton()
        {
            GetReportUtils().ClickButton("Add Resource");
            GetWebElementUtils().clickWebElement(addResourceButton);
            return new AddResourceWindow(GetBaseInformation());
        }

        private void EnterSearchResourceText(String searchText)
        {
            GetReportUtils().EnterValueToField("Search resource", searchText);
            searchResourceField.SendKeys(searchText);
            Thread.Sleep(3000);
        }

        private void ClickSearchResourceButton()
        {
            GetReportUtils().ClickButton("Search resource");
            GetWebElementUtils().clickWebElement(searchResourceButton);
            Thread.Sleep(2000);
            GetWaitUtils().WaitForElementInvisible(By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbResourceTab_LPV"));
            Thread.Sleep(2000);
        }

        public void SearchResource(String resource)
        {
            EnterSearchResourceText(resource);
            ClickSearchResourceButton();
        }

        public IList<ResourceRow> GetResourceRows()
        {
            IList<ResourceRow> result = new List<ResourceRow>();
            foreach (IWebElement row in resourceRootRows)
            {
                result.Add(new ResourceRow(GetBaseInformation(), row));
            }
            return result;
        }

        public IList<ResourceRow> GetResourceRowsByResourceName(String name)
        {
            return GetResourceRows().Where(r => r.GetResourceName().Contains(name)).ToList();
        }

        public AddEquipmentWindow ClickAddEquipmentButton()
        {
            GetReportUtils().ClickButton("Add equipment");
            GetWebElementUtils().clickWebElement(addEquimentButton);
            return new AddEquipmentWindow(GetBaseInformation());
        }

        public IList<EquipmentRow> GetEquipmentRows()
        {
            IList<EquipmentRow> result = new List<EquipmentRow>();
            foreach (IWebElement element in equipmentRows)
            {
                result.Add(new EquipmentRow(GetBaseInformation(), element));
            }
            return result;
        }

        public IList<EquipmentRow> GetEquipmentRowByLicensePlateEquipmentId(String licensePlateEquipmentId)
        {
            return GetEquipmentRows().Where(r => r.GetLicensePlateEquipmentId().Contains(licensePlateEquipmentId)).ToList();
        }

        public void DragResourceIntoOrganization(ResourceRow resourceRow, OrganizationRow organizationRow)
        {
            GetReportUtils().AllureStepWithPageObject("Drag and drop resource row into organization row");
            Actions act = new Actions(GetBaseInformation().GetDriver());
            act.DragAndDrop(resourceRow.GetNameLabel(), organizationRow.GetNameLabel())
                .Build()
                .Perform();

        }

        public void DragEqupmentIntoOrganization(EquipmentRow equpmentRow, OrganizationRow organizationRow)
        {
            GetReportUtils().AllureStepWithPageObject("Drag and drop equipment row into organization row");
            Actions act = new Actions(GetBaseInformation().GetDriver());
            act.ClickAndHold(equpmentRow.GetTypeLabel())
                .Pause(TimeSpan.FromSeconds(5))
                .MoveToElement(organizationRow.GetEqupmentPlace())
                .Pause(TimeSpan.FromSeconds(5))
                .Release()
                .Build()
                .Perform();
            Thread.Sleep(TimeSpan.FromSeconds(10));
        }

        public IList<OrganizationRow> GetOrganizationRows()
        {
            IList<OrganizationRow> result = new List<OrganizationRow>();
            foreach (IWebElement row in organizationRows)
            {
                result.Add(new OrganizationRow(GetBaseInformation(), row));
            }
            return result;
        }

        public IList<OrganizationRow> GetOrganizationRowsByName(String name)
        {
            return GetOrganizationRows().Where(r => r.GetName().Equals(name)).ToList();
        }

        public void ClickSaveButton()
        {
            GetReportUtils().ClickButton("Save");
            GetWebElementUtils().clickWebElement(saveButton);
            Thread.Sleep(10000);
        }

        public class EquipmentRow : BasePageObject
        {

            [FindsBy(How = How.XPath, Using = ".//td[1]")]
            private IWebElement typeLabel;

            [FindsBy(How = How.XPath, Using = ".//td[3]/span")]
            private IWebElement licensePlateEquipmentIdLabel;

            public EquipmentRow(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Equipment", "row"))
            {
            }

            public String GetLicensePlateEquipmentId()
            {
                return licensePlateEquipmentIdLabel.Text.Trim();
            }

            public IWebElement GetTypeLabel()
            {
                return typeLabel;
            }

        }

        public class ResourceRow : BasePageObject
        {

            [FindsBy(How = How.XPath, Using = ".//td[1]//span")]
            private IWebElement resourceNameLabel;

            public ResourceRow(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Resource", "row"))
            {
            }

            public String GetResourceName()
            {
                return resourceNameLabel.Text.Trim();
            }

            public IWebElement GetNameLabel()
            {
                return resourceNameLabel;
            }

        }

        public class OrganizationRow : BasePageObject
        {

            [FindsBy(How = How.CssSelector, Using = "span[type='super_intendent'],span[type='general_foreman'],span[type='support'],span[type='foreman'],div.node_droparea.ui-draggable.ui-draggable-handle.ui-droppable > span")]
            private IWebElement nameLabel;

            [FindsBy(How = How.CssSelector, Using = "div.node_droparea2.ui-draggable")]
            private IWebElement equpmentPlace;

            public OrganizationRow(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Organization", "row"))
            {
            }

            public String GetName()
            {
                return nameLabel.Text.Trim();
            }

            public IWebElement GetNameLabel()
            {
                return nameLabel;
            }

            public IWebElement GetEqupmentPlace()
            {
                return equpmentPlace;
            }

            public Boolean IsEquipmentPresent()
            {
                try
                {
                    equpmentPlace.FindElement(By.CssSelector("span"));
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

    }
}
