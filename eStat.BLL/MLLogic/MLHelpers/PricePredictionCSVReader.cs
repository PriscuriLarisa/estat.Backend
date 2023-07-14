using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace eStat.BLL.MLLogic.MLHelpers
{
    public static class PricePredictionCSVReader
    {
        private const string PREDICT_FILE_PATH = "..\\eStat.BLL\\MLLogic\\SolutionItems\\test.csv";

        public static List<Library.Models.PricePrediction> GetListOfPricePredictions()
        {
            List<string> headers = ColumnNameGenerator(PREDICT_FILE_PATH);

            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };
            List<Library.Models.PricePrediction> pricePredictions = new List<Library.Models.PricePrediction>();
            CsvReader csv = new CsvReader(File.OpenText(PREDICT_FILE_PATH), csvConfiguration);
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                Library.Models.PricePrediction pricePredictionModel = new();
                pricePredictionModel.UserProductID = Guid.Parse((csv.GetField<string>("UserProductID") ?? Guid.Empty.ToString()).Trim('"'));
                pricePredictionModel.NbOfSearchesLastMonth = Int32.Parse((csv.GetField<string>("NbOfSearchesLastMonth") ?? "0").Trim('"'));
                pricePredictionModel.NbOfPurchasesLastMonth = Int32.Parse((csv.GetField<string>("NbOfPurchasesLastMonth") ?? "0").Trim('"'));
                pricePredictionModel.MyStock = Int32.Parse(csv.GetField<string>("MyStock") ?? "0");
                pricePredictionModel.MySellsLastMonth = Int32.Parse((csv.GetField<string>("MySellsLastMonth") ?? "0"));
                pricePredictionModel.CurrentAveragePrice = Math.Round(Decimal.Parse((csv.GetField<string>("CurrentAveragePrice") ?? "0.0"), CultureInfo.InvariantCulture), 2);
                pricePredictionModel.AveragePriceLastMonth = Math.Round(Decimal.Parse((csv.GetField<string>("AveragePriceLastMonth") ?? "0.0"), CultureInfo.InvariantCulture), 2);
                pricePredictionModel.CurrentAverageStockPerRetailer = Math.Round(Decimal.Parse((csv.GetField<string>("CurrentAverageStockPerRetailer") ?? "0.0"), CultureInfo.InvariantCulture), 2);
                pricePredictionModel.AverageStockPerRetailerLastMonth = Math.Round(Decimal.Parse((csv.GetField<string>("AverageStockPerRetailerLastMonth") ?? "0.0"), CultureInfo.InvariantCulture), 2);
                pricePredictionModel.MyCurrentPrice = Math.Round(Decimal.Parse((csv.GetField<string>("MyCurrentPrice") ?? "0.0"), CultureInfo.InvariantCulture), 2);
                pricePredictionModel.MyAveragePriceLastMonth = Math.Round(Decimal.Parse((csv.GetField<string>("MyAveragePriceLastMonth") ?? "0.0"), CultureInfo.InvariantCulture), 2);
                pricePredictionModel.PredictedPrice = Math.Round(Decimal.Parse((csv.GetField<string>("PredictedPrice") ?? "0.0"), CultureInfo.InvariantCulture), 2);

                pricePredictions.Add(pricePredictionModel);
            }

            return pricePredictions;
        }

        private static List<string> ColumnNameGenerator(string FilePath)
        {
            string firstLine = "";
            using (StreamReader reader = new StreamReader(FilePath))
            {
                firstLine = reader.ReadLine() ?? "";
            }
            return firstLine.Replace('"', ' ').Split(',').Select(x => x.Trim()).ToList();
        }
    }
}
