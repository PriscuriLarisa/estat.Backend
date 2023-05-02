using eStat.Library.Models;
using ExcelDataReader;
using System.Globalization;
using System.Text;

namespace eStat.Library.Helpers
{
    public static class ExcelHelper
    {
        private readonly static string PRODUCTS_FILE_PATH =
            "D:\\Work\\Uni\\Licenta\\eStat\\eStat\\eStat.Library\\SolutionItems\\Dataset.xlsx";

        public static List<Product> GetProductsFromDataset()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using var stream = File.Open(PRODUCTS_FILE_PATH, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            CultureInfo c2 = new CultureInfo("en-US");

            List<Product> products = new List<Product>();
            bool hasValues = true;
            do
            {
                int index = 0;
                while (reader.Read())
                {
                    index++;
                    if (index == 1)
                    {
                        continue;
                    }
                    if(reader.GetValue(4) == null)
                    {
                        hasValues = false;
                        break;
                    }
                    Product product = new()
                    {
                        ProductGUID = Guid.NewGuid(),
                        Name = reader.GetValue(4).ToString()!,
                        Category = reader.GetValue(11).ToString()!,
                        InUse = true,
                        BasePrice = decimal.Parse(reader.GetValue(12).ToString()!, c2),
                        ImageLink = reader.GetValue(8).ToString()!.Split(' ')[0],
                        Characteristics = reader.GetValue(9).ToString()!
                    };
                    products.Add(product);
                }
            } while (reader.NextResult());
            return products;
        }
    }
}
