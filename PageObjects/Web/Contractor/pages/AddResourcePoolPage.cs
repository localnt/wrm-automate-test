using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.BaseElements;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.pages
{
    public class AddResourcePoolPage : BaseLoggedPage
    {

        [FindsBy(How = How.CssSelector, Using = "input[name*='nbckit_resource_pool_name_']")]
        private IWebElement resourcePoolName;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_tblc_nbckit_source_location_id")]
        private IWebElement sourceLocationDropdownRootElement;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_chbIsOnIOU_S_D")]
        private IWebElement onIoyCheckbox;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_Td1")]
        private IWebElement utilityDropdownRootElement;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_nbckit_est_total_resource_ASPxSpinEdit_I")]
        private IWebElement esimatedResourcesField;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_spe_nbckit_est_total_crew_I")]
        private IWebElement estimatedCrewField;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_speCrewSize_I")]
        private IWebElement crewSize;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_spe_nbckit_est_total_bucket_I")]
        private IWebElement estimatedBuckets;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_spe_nbckit_est_total_digger_I")]
        private IWebElement estimatedDiggers;

        [FindsBy(How = How.CssSelector, Using = "div[title='Add new resource pool']")]
        private IWebElement addButton;

        public AddResourcePoolPage(BaseInformation baseInformation) : base(baseInformation, new ReportUtils(baseInformation, "Add new Resource pool", "page"))
        {

        }

        public void EnterResourcePoolName(String name)
        {
            GetReportUtils().EnterValueToField("Resource Pool Name", name);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(resourcePoolName, name);
        }

        public StandardDropdownMenu GetSourceLocationDropDownMenu()
        {
            return new StandardDropdownMenu(GetBaseInformation(), sourceLocationDropdownRootElement, new ReportUtils(GetBaseInformation(), "Source Location", "Dropdown menu"));
        }

        public Boolean IsOnIouCheckboxChecked()
        {
            return onIoyCheckbox.GetAttribute("class")
                .Contains("CheckBoxChecked");
        }

        public void UncheckOnIouCheckbox()
        {
            GetReportUtils().AllureStepWithPageObject("Uncheck \"On - IOU\" checkbox");
            if (IsOnIouCheckboxChecked())
            {
                GetWebElementUtils().clickWebElement(onIoyCheckbox);
            }
        }

        public void CheckOnIouCheckbox()
        {
            GetReportUtils().AllureStepWithPageObject("Check \"On - IOU\" checkbox");
            if (!IsOnIouCheckboxChecked())
            {
                GetWebElementUtils().clickWebElement(onIoyCheckbox);
            }
        }

        public StandardDropdownMenu GetUtilityDropDownMenu()
        {
            return new StandardDropdownMenu(GetBaseInformation(), utilityDropdownRootElement, new ReportUtils(GetBaseInformation(), "Utility", "Dropdown menu"));
        }

        public void EnterEstimatedResources(int resources)
        {
            GetReportUtils().EnterValueToField("Estimated Resources", resources.ToString());
            GetWebElementUtils().enterValueToFieldWithWaitEntering(esimatedResourcesField, resources.ToString());
        }

        public void EnterExtimatedCrews(int crews)
        {
            GetReportUtils().EnterValueToField("Estimated Crews", crews.ToString());
            GetWebElementUtils().enterValueToFieldWithWaitEntering(estimatedCrewField, crews.ToString());
        }

        public void EnterCrewSize(int crewSizeValue)
        {
            GetReportUtils().EnterValueToField("Crew size", crewSizeValue.ToString());
            GetWebElementUtils().enterValueToFieldWithWaitEntering(crewSize, crewSizeValue.ToString());
        }

        public void EnterEstimatedBuckets(int buckets)
        {
            GetReportUtils().EnterValueToField("Estimated Buckets", buckets.ToString());
            GetWebElementUtils().enterValueToFieldWithWaitEntering(estimatedBuckets, buckets.ToString());
        }

        public void EnterEstimatedDiggers(int diggers)
        {
            GetReportUtils().EnterValueToField("Estimated Diggers", diggers.ToString());
            GetWebElementUtils().enterValueToFieldWithWaitEntering(estimatedDiggers, diggers.ToString());
        }

        public InternalWorkforcePage ClickAddButton()
        {
            GetReportUtils().ClickButton("Add");
            GetWebElementUtils().clickWebElement(addButton);
            Thread.Sleep(30000);
            return new InternalWorkforcePage(GetBaseInformation());
        }


    }
}
