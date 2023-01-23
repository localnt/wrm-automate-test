using ExcelDataReader;
using System.Data;

namespace WRMAutotests.Utility
{
    public class ExcelReadedUtils
    {

        private IExcelDataReader excelReader;
        private DataSet result;
        private String nameOfFile;

        public ExcelReadedUtils(String nameOfFileInPropertiesFolder)
        {
            nameOfFile = nameOfFileInPropertiesFolder;
            String pathToFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"Properties\", nameOfFileInPropertiesFolder);
            FileStream stream = File.Open(pathToFile, FileMode.Open, FileAccess.Read);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            if (Path.GetExtension(pathToFile).ToUpper() == ".XLS")
            {
                //Reading from a binary Excel file ('97-2003 format; *.xls)
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else
            {
                //Reading from a OpenXml Excel file (2007 format; *.xlsx)
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            result = excelReader.AsDataSet();
            excelReader.Close();
            stream.Close();
        }

        public String GetCellValue(int numberOFTable, int numberOfRow, int numberOfColumn)
        {

            String resultValue = result.Tables[numberOFTable].Rows[numberOfRow][numberOfColumn].ToString();
            resultValue = resultValue.Trim();
            if (resultValue.Equals("FAULT"))
                resultValue = "FALSE";
            System.Diagnostics.Debug.WriteLine("Read from excel file: " + nameOfFile + ". Number of table = " + numberOFTable + " Number of row: " + numberOfRow + " Number of Column: " + numberOfColumn + " Value: " + resultValue);
            return resultValue;
        }

    }
}
