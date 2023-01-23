using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.BaseElements;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.windows
{
    public class AddResourceWindow : BasePageObject
    {

        private static By rootLocator = By.CssSelector("div[id*='cpnAddResource_pucAddResource'].dxpcLite_DevEx.dxpclW");

        [FindsBy(How = How.CssSelector, Using = ".dxic input[name*='first_name']")]
        private IWebElement firstNameField;

        [FindsBy(How = How.CssSelector, Using = ".dxic input[name*='last_name']")]
        private IWebElement lastNameField;

        [FindsBy(How = How.CssSelector, Using = "td[id*='classification_id']")]
        private IWebElement classificationRootMenuElement;

        [FindsBy(How = How.CssSelector, Using = " div[title='Add Resource']")]
        private IWebElement addResourceButton;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbResourceTab_cpnAddResource_pucAddResource_tblc_union_id")]
        private IWebElement unionMenuRootElement;

        [FindsBy(How = How.XPath, Using = ".//table[./tbody/tr/td/label[text()='Female']]")]
        private IWebElement genderFemaleButton;

        [FindsBy(How = How.XPath, Using = ".//table[./tbody/tr/td/label[text()='Male']]")]
        private IWebElement genderMaleButton;

        public AddResourceWindow(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Add Resource", "window"))
        {
        }

        public void ClickMaleButton()
        {
            GetReportUtils().ClickButton("Male");
            genderMaleButton.Click();
        }

        public void ClickFemaleButton()
        {
            GetReportUtils().ClickButton("Female");
            genderFemaleButton.Click();
        }

        public void EnterFirstName(String name)
        {
            GetReportUtils().EnterValueToField("First name", name);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(firstNameField, name);
        }

        public void EnterLastName(String name)
        {
            GetReportUtils().EnterValueToField("Last name", name);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(lastNameField, name);
        }

        public void SelectUnion(String union)
        {
            GetReportUtils().AllureStepWithPageObject("Select Union: " + union);
            new StandardDropdownMenu(GetBaseInformation(), unionMenuRootElement, new ReportUtils(GetBaseInformation(), "Union", "dropdown menu")).SelectMenuElement(union);
        }

        public void SelectClassification(String classification)
        {
            GetReportUtils().AllureStepWithPageObject("Select classification: " + classification);
            new StandardDropdownMenu(GetBaseInformation(), classificationRootMenuElement, new ReportUtils(GetBaseInformation(), "Classification", "dropdown menu")).SelectMenuElement(classification);
        }

        public void ClickAddResource()
        {
            GetReportUtils().ClickButton("Add resource");
            GetWebElementUtils().clickWebElement(addResourceButton);
            Thread.Sleep(15000);
        }


    }
}
