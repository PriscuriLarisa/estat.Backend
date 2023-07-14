namespace eStat.Library.Converters
{
    public static class PricePredictionConverter
    {
        public static Library.Models.PricePrediction ToDTO(DAL.Entities.PricePrediction pricePrediction)
        {
            return new Library.Models.PricePrediction
            {
                PricePredictionGUID= pricePrediction.PricePredictionGUID,
                PredictedPrice = pricePrediction.PredictedPrice,
                AveragePriceLastMonth= pricePrediction.AveragePriceLastMonth,
                AverageStockPerRetailerLastMonth = pricePrediction.AverageStockPerRetailerLastMonth,
                CurrentAverageStockPerRetailer = pricePrediction.CurrentAverageStockPerRetailer,
                CurrentAveragePrice = pricePrediction.CurrentAveragePrice,
                MyAveragePriceLastMonth= pricePrediction.MyAveragePriceLastMonth,
                MyCurrentPrice = pricePrediction.MyCurrentPrice,
                NbOfPurchasesLastMonth = pricePrediction.NbOfPurchasesLastMonth,
                UserProductID=pricePrediction.UserProductID,
                MySellsLastMonth= pricePrediction.MySellsLastMonth,
                MyStock = pricePrediction.MyStock,
                NbOfSearchesLastMonth = pricePrediction.NbOfSearchesLastMonth
            };
        }

        public static DAL.Entities.PricePrediction ToEntity(Library.Models.PricePrediction pricePrediction)
        {
            return new DAL.Entities.PricePrediction
            {
                PricePredictionGUID = pricePrediction.PricePredictionGUID,
                PredictedPrice = pricePrediction.PredictedPrice,
                AveragePriceLastMonth = pricePrediction.AveragePriceLastMonth,
                AverageStockPerRetailerLastMonth = pricePrediction.AverageStockPerRetailerLastMonth,
                CurrentAverageStockPerRetailer = pricePrediction.CurrentAverageStockPerRetailer,
                CurrentAveragePrice = pricePrediction.CurrentAveragePrice,
                MyAveragePriceLastMonth = pricePrediction.MyAveragePriceLastMonth,
                MyCurrentPrice = pricePrediction.MyCurrentPrice,
                NbOfPurchasesLastMonth = pricePrediction.NbOfPurchasesLastMonth,
                UserProductID = pricePrediction.UserProductID,
                MySellsLastMonth = pricePrediction.MySellsLastMonth,
                MyStock = pricePrediction.MyStock,
                NbOfSearchesLastMonth = pricePrediction.NbOfSearchesLastMonth
            };
        }
    }
}
