namespace WRMAutotests.Utility
{

    public class RandomValuesUtilities
    {
        private String basePart = "CreatedByTest";



        public RandomValuesUtilities()
        {

        }

        public RandomValuesUtilities(String basePart)
        {
            this.basePart = basePart;
        }


        public String GetRandomValue()
        {
            return basePart + GenerateRandomNumber(100000, 999999);
        }

        public int GenerateRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }


    }
}
