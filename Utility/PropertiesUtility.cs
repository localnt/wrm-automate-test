namespace WRMAutotests.Utility
{
    public class PropertiesUtility
    {
        private String pathToFile;
        private Dictionary<string, string> properties = new Dictionary<string, string>();

        public PropertiesUtility(String fileName)
        {
            pathToFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"Properties\", fileName);
            foreach (String row in File.ReadAllLines(pathToFile))
            {
                properties.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
            }
        }

        public String getPropertyValue(String key)
        {
            return properties[key];
        }

    }
}
