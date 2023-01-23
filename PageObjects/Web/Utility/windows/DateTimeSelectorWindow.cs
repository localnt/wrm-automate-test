using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Globalization;
using WRMAutotests.PageObjects.Web.Base;
using WRMAutotests.Utility;
using WRMAutotests.Utility.Web;

namespace WRMAutotests.PageObjects.Web.Utility.windows
{
    public class DateTimeSelectorWindow : BasePageObject
    {

        private CultureInfo provider = CultureInfo.InvariantCulture;

        [FindsBy(How = How.CssSelector, Using = "td[id*='_DDD_C_TC'] > span")]
        private IWebElement monthAndYearLabel;

        [FindsBy(How = How.CssSelector, Using = "td[onclick*='CalShiftMonth'] > img[class*='edtCalendarNextMonth']")]
        private IWebElement nextMonthButton;

        [FindsBy(How = How.CssSelector, Using = "td[onclick*='CalShiftMonth'] > img[class*='edtCalendarPrevMonth']")]
        private IWebElement previousMonthButton;

        [FindsBy(How = How.CssSelector, Using = ".dxeCalendarDay_DevEx:not(.dxeCalendarOtherMonth_DevEx)")]
        private IList<IWebElement> days;

        [FindsBy(How = How.CssSelector, Using = "input.dxeEditArea_DevEx.dxeEditAreaSys")]
        private IWebElement timeInput;

        [FindsBy(How = How.XPath, Using = ".//td[text()='OK']")]
        public IWebElement okButton;

        public DateTimeSelectorWindow(BaseInformation baseInformation, IWebElement rootElement) : base(baseInformation, rootElement, new ReportUtils(baseInformation, "Date Time selection", "window"))
        {
        }

        private DateTime GetSelectedMonthAndYear()
        {
            String monthYearText = monthAndYearLabel.Text;
            try
            {
                return DateTime.ParseExact(monthYearText, "MMMM, yyyy", provider);
            }
            catch (System.FormatException ex)
            {
                return DateTime.ParseExact(monthYearText, "MMMM yyyy", provider);
            }

        }

        private void ClickNextMonthButton()
        {
            GetWebElementUtils().clickWebElement(nextMonthButton);
            Thread.Sleep(1000);
        }

        private void ClickPrevMonthButton()
        {
            GetWebElementUtils().clickWebElement(previousMonthButton);
            Thread.Sleep(1000);
        }

        private void SelectMonthAndYear(DateTime dateTime)
        {
            DateTime selectedDateTime = GetSelectedMonthAndYear();
            int monthDifference = (dateTime.Month + dateTime.Year * 12) - (selectedDateTime.Month + selectedDateTime.Year * 12);
            if (monthDifference == 0)
                return;
            if (monthDifference > 0)
            {
                for (int i = 0; i < monthDifference; i++)
                {
                    ClickNextMonthButton();
                }
            }
            else
            {
                monthDifference = monthDifference * -1;
                for (int i = 0; i < monthDifference; i++)
                {
                    ClickPrevMonthButton();
                }
            }
        }

        private void ClickTargetDayOfMonth(DateTime dateTime)
        {
            IWebElement targetDay = days.Where(d => d.Text.Trim().Equals("") ? false : d.Text.Trim().Equals(dateTime.Day.ToString())).First();
            GetWebElementUtils().clickWebElement(targetDay);
            Thread.Sleep(1000);
        }

        private void EnterTime(DateTime time)
        {
            timeInput.Click();
            for (int i = 0; i < 8; i++)
            {
                timeInput.SendKeys(Keys.Backspace);
                Thread.Sleep(200);
            }
            timeInput.SendKeys(time.ToString("hhmmtt", provider));
            Thread.Sleep(2000);
        }

        private void ClickOkButton()
        {
            GetWebElementUtils().clickWebElement(okButton);
            Thread.Sleep(1000);
        }

        public void SelectDate(DateTime dateTime)
        {
            SelectMonthAndYear(dateTime);
            ClickTargetDayOfMonth(dateTime);
            EnterTime(dateTime);
            ClickOkButton();
        }


    }
}
