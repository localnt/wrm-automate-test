using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.PageObjects.Web.BaseElements;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Contractor.windows
{
    public class AddEquipmentWindow : BasePageObject
    {
        private static By rootLocator = By.CssSelector("div#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbEquipmentTab_cpnAddEquipment_pucAddEquipment_PW-1");

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbEquipmentTab_cpnAddEquipment_pucAddEquipment_cbEquipmentType_CC")]
        private IWebElement typeSelectorRoot;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbEquipmentTab_cpnAddEquipment_pucAddEquipment_eqmt_equipment_sub_type_id_ASPxComboBox_CC")]
        private IWebElement subTypeSelector;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbEquipmentTab_cpnAddEquipment_pucAddEquipment_eqmt_physical_id_ASPxTextBox_I")]
        private IWebElement licensePlateField;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbEquipmentTab_cpnAddEquipment_pucAddEquipment_eqmt_license_state_ASPxComboBox_ET")]
        private IWebElement licenseStateSelectorRoot;

        [FindsBy(How = How.CssSelector, Using = "#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbEquipmentTab_cpnAddEquipment_pucAddEquipment_eqmt_equipement_id_ASPxTextBox_I")]
        private IWebElement equipmentIdField;

        [FindsBy(How = How.CssSelector, Using = "div[title*='Add Equipment']")]
        private IWebElement addEqupmentButton;


        public AddEquipmentWindow(BaseInformation baseInformation) : base(baseInformation, rootLocator, new ReportUtils(baseInformation, "Add Equipment", "window"))
        {
        }

        public void SelectType(String type)
        {
            new StandardDropdownMenu(GetBaseInformation(), typeSelectorRoot, new ReportUtils(GetBaseInformation(), "Type", "Dropdown menu")).SelectMenuElement(type);
        }

        public void SelectSubType(String subtype)
        {
            new StandardDropdownMenu(GetBaseInformation(), subTypeSelector, new ReportUtils(GetBaseInformation(), "Sub Type", "Dropdown menu")).SelectMenuElement(subtype);
        }

        public void EnterLicensePlate(String licensePlate)
        {
            GetReportUtils().EnterValueToField("License plate", licensePlate);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(licensePlateField, licensePlate);
        }

        public void SelectLicenseState(String licenseState)
        {
            new StandardDropdownMenu(GetBaseInformation(), licenseStateSelectorRoot, new ReportUtils(GetBaseInformation(), "License State", "Dropdown menu")).SelectMenuElement(licenseState);
        }

        public void EnterEquipmentId(String equipmentId)
        {
            GetReportUtils().EnterValueToField("Equipment ID", equipmentId);
            GetWebElementUtils().enterValueToFieldWithWaitEntering(equipmentIdField, equipmentId);
        }

        public void ClickAddEquipment()
        {
            GetReportUtils().ClickButton("Add Equipment");
            GetWebElementUtils().clickWebElement(addEqupmentButton);
            Thread.Sleep(2000);
            GetWaitUtils().WaitForElementInvisible(By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_cpnResourceEquipment_ASPxPageControl_cbEquipmentTab_LPV"));
            Thread.Sleep(10000);
        }


    }
}
