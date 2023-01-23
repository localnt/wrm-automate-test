using OpenQA.Selenium;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.Tests.StabilityScript
{
    public class StabilityScripts
    {

        public static void DEV_1_MT_ContractorAllMenu(BaseInformation baseInformation, String baseUrl, User user)
        {
            IWebDriver driver = baseInformation.GetDriver();

            //login as contractor
            driver.Navigate().GoToUrl(baseUrl + "Public/login.aspx?ReturnUrl=%2f");
            Assert.That(driver.Title, Is.EqualTo("Login - Storm Manager"));
            driver.FindElement(By.Id("ASPxPanel2_ContentPlaceHolder1_LoginControl1_Login1_Username_I")).SendKeys(user.GetEmail());
            driver.FindElement(By.Id("ASPxPanel2_ContentPlaceHolder1_LoginControl1_Login1_Password_I")).SendKeys(user.GetPassword());
            driver.FindElement(By.CssSelector(".dx-vam:nth-child(3)")).Click();

            //open all need pages
            Dictionary<String, String> pagesAndTitles = new Dictionary<String, String>();
            pagesAndTitles.Add("Private/Workforce/resourcebuckets.aspx", "Internal Workforce");
            pagesAndTitles.Add("Private/Workforce/resources.aspx", "Resources");
            pagesAndTitles.Add("Private/Workforce/equipments.aspx", "Equipment");
            pagesAndTitles.Add("Private/Workforce/equipmentsubtypes.aspx", "Equipment Subtype");
            pagesAndTitles.Add("Private/Workforce/procurementrequests.aspx", "Procurement Request");
            pagesAndTitles.Add("Private/Expense/expensecenter.aspx", "Expense Center");
            pagesAndTitles.Add("Private/TimeSheet/timesheetcenter.aspx", "Time Sheet Center");
            pagesAndTitles.Add("Private/Financial/invoicecenter.aspx", "");
            pagesAndTitles.Add("Private/Setting/users.aspx", "Users page");
            pagesAndTitles.Add("Private/Setting/roles.aspx", "Roles Page");
            pagesAndTitles.Add("Private/Financial/contract.aspx", "Contract");
            pagesAndTitles.Add("Private/Setting/contractorevaluationweighting.aspx", "Contractor Evaluation Weighting");
            pagesAndTitles.Add("Private/Setting/hotelevaluationweighting.aspx", "Hotel Evaluation Weighting");

            foreach (KeyValuePair<String, String> partOfUrlAndTitle in pagesAndTitles)
            {
                driver.Navigate().GoToUrl(baseUrl + partOfUrlAndTitle.Key);
                Thread.Sleep(5000);
                if (!partOfUrlAndTitle.Value.Equals(""))
                {
                    Assert.That(driver.Title, Is.EqualTo(partOfUrlAndTitle.Value));
                }
            }

        }

        public static void DEV_2_NG_SCS_ALL_MENU(BaseInformation baseInformation, String baseUrl, User user)
        {
            IWebDriver driver = baseInformation.GetDriver();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            IDictionary<string, object> vars = new Dictionary<string, object>();

            //login to Utility NG
            driver.Navigate().GoToUrl(baseUrl + "default.aspx");
            Assert.That(driver.Title, Is.EqualTo("Welcome to Storm Manager"));
            driver.FindElement(By.Id("AdvanceSearch_ASPxButton1_CD")).Click();
            vars["title"] = driver.Title;
            if ((Boolean)js.ExecuteScript("return (arguments[0] == \'Event Selection\')", vars["title"]))
            {
                driver.FindElement(By.Id("ASPxPanel2_ContentPlaceHolder1_gluOC_B-1Img")).Click();
                driver.FindElement(By.Id("ASPxPanel2_ContentPlaceHolder1_gluOC_DDD_gv_tccell3_0")).Click();
                driver.FindElement(By.Id("ASPxPanel2_ContentPlaceHolder1_gluEvents_I")).Click();
                driver.FindElement(By.Id("ASPxPanel2_ContentPlaceHolder1_gluEvents_B-1Img")).Click();
                driver.FindElement(By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_gluEvents_DDD_gv_tccell2_0 .EventDropdownColEventName")).Click();
            }
            else if ((Boolean)js.ExecuteScript("return (arguments[0] == \'Login - Storm Manager\')", vars["title"]))
            {
                driver.FindElement(By.Id("username")).SendKeys(user.GetEmail());
                driver.FindElement(By.Id("password")).SendKeys(user.GetPassword());
                driver.FindElement(By.Id("btn_VendorLogin")).Click();
                Thread.Sleep(5000);
                driver.FindElement(By.Id("ASPxPanel2_ContentPlaceHolder1_gluOC_B-1Img")).Click();
                driver.FindElement(By.Id("ASPxPanel2_ContentPlaceHolder1_gluOC_DDD_gv_tccell3_0")).Click();
                Thread.Sleep(5000);
                driver.FindElement(By.Id("ASPxPanel2_ContentPlaceHolder1_gluEvents_I")).Click();
                driver.FindElement(By.Id("ASPxPanel2_ContentPlaceHolder1_gluEvents_B-1Img")).Click();
                driver.FindElement(By.CssSelector("#ASPxPanel2_ContentPlaceHolder1_gluEvents_DDD_gv_tccell2_0 .EventDropdownColEventName")).Click();
                Thread.Sleep(10000);
            }
            Assert.That(driver.Title, Is.EqualTo("Manage Secured Workforce"));

            //open all need pages
            Dictionary<String, String> pagesAndTitles = new Dictionary<String, String>();
            pagesAndTitles.Add("Private/Workforce/resourcebuckets.aspx", "Internal Workforce");
            pagesAndTitles.Add("Private/Workforce/resources.aspx", "Resources");
            pagesAndTitles.Add("Private/Workforce/equipments.aspx", "Equipment");
            pagesAndTitles.Add("Private/Workforce/equipmentsubtypes.aspx", "Equipment Subtype");
            pagesAndTitles.Add("Private/Workforce/crewavailabilityrequest.aspx", "Crew Availability Request");
            pagesAndTitles.Add("Private/Workforce/crewavailabilityreport.aspx", "Non-IOU Marketplace");
            pagesAndTitles.Add("Private/Report/crewlocationreport.aspx", "Crew Location Report");
            pagesAndTitles.Add("Private/Report/workforcecustompivotreport.aspx", "Workforce Custom Pivot Report");
            pagesAndTitles.Add("Private/Report/customizedreportextract.aspx", "Customized Report Extract");
            pagesAndTitles.Add("Private/Setting/users.aspx", "Users page");
            pagesAndTitles.Add("Private/Setting/roles.aspx", "Roles Page");
            pagesAndTitles.Add("Private/Financial/contract.aspx", "Contract");
            pagesAndTitles.Add("Private/Financial/unionagreement.aspx", "Union Agreement");
            pagesAndTitles.Add("Private/Setting/documentrequirements.aspx", "Document Requirements");
            pagesAndTitles.Add("Private/Setting/managetags.aspx", "Manage Tags");
            pagesAndTitles.Add("Private/Setting/templatemanagement.aspx", "Template Management");
            pagesAndTitles.Add("Private/Setting/contractorevaluationweighting.aspx", "Contractor Evaluation Weighting");
            pagesAndTitles.Add("Private/Setting/hotelevaluationweighting.aspx", "Hotel Evaluation Weighting");

            foreach (KeyValuePair<String, String> partOfUrlAndTitle in pagesAndTitles)
            {
                driver.Navigate().GoToUrl(baseUrl + partOfUrlAndTitle.Key);
                Thread.Sleep(5000);
                if (!partOfUrlAndTitle.Value.Equals(""))
                {
                    Assert.That(driver.Title, Is.EqualTo(partOfUrlAndTitle.Value));
                }
            }

        }


    }
}
