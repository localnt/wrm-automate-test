namespace WRMAutotests.Utility
{
    public class AssertionUtils
    {

        private ReportUtils reportUtils;

        public AssertionUtils(ReportUtils reportUtils)
        {
            this.reportUtils = reportUtils;
        }

        public void TrueAssertionWithoutNameOfPageObject(String description, Boolean value)
        {
            String assertionBaseDescription = "Verify that";
            if (!description.Contains(assertionBaseDescription))
            {
                throw new AssertionException("Description for assertion dont contain: " + assertionBaseDescription);
            }
            reportUtils.AllureStep(description);
            Assert.IsTrue(value);
        }

        public void TrueAssertion(String description, Boolean value)
        {
            String assertionBaseDescription = "Verify that";
            if (!description.Contains(assertionBaseDescription))
            {
                throw new AssertionException("Description for assertion dont contain: " + assertionBaseDescription);
            }
            reportUtils.AllureStepWithPageObject(description);
            Assert.IsTrue(value);
        }

        public void EquialAssertion(String description, Object first, Object second)
        {
            String assertionBaseDescription = "Verify that";
            if (!description.Contains(assertionBaseDescription))
            {
                throw new AssertionException("Description for assertion dont contain: " + assertionBaseDescription);
            }
            reportUtils.AllureStepWithPageObject(description + ". Expected: " + first.ToString() + ". Found value: " + second.ToString());
            Assert.That(first, Is.EqualTo(second));
        }



    }
}
