using CsvHelper;
using eStat.BLL.Interfaces;
using eStat.BLL.MLLogic.MLHelpers.Interfaces;
using eStat.Library.Models;
using ExcelDataReader.Log;
using Microsoft.Extensions.DependencyInjection;

namespace eStat.BLL.MLLogic.MLHelpers
{
    public class PricePredictionHandler : IPricePredictionHandler
    {
        private readonly IPricePredictions _pricePredictionsService;
        private readonly INotifications _notificationsService;
        private readonly IUserProducts _userProductsService;
        private readonly string _trainDatasetFilePath = "..\\eStat.BLL\\MLLogic\\SolutionItems\\train.csv";
        private readonly string _predictDatasetFilePath = "..\\eStat.BLL\\MLLogic\\SolutionItems\\test.csv";

        public PricePredictionHandler(IServiceScopeFactory factory)
        {
            _pricePredictionsService = factory.CreateScope().ServiceProvider.GetRequiredService<IPricePredictions>();
            _notificationsService = factory.CreateScope().ServiceProvider.GetRequiredService<INotifications>();
            _userProductsService = factory.CreateScope().ServiceProvider.GetRequiredService<IUserProducts>();
        }

        public void HandlePricePredictions(List<PricePrediction> pricePredictions)
        {
            foreach (var pricePrediction in pricePredictions)
            {
                UserProduct up = _userProductsService.GetByUid(pricePrediction.UserProductID);
                string productName = up.Product.Name;
                pricePrediction.PricePredictionGUID = Guid.Empty;
                PricePrediction createdPricePrediction = _pricePredictionsService.Add(pricePrediction);
                Notification notification = new Notification
                {
                    NotificationGUID = Guid.Empty,
                    Title = "Price recommendation",
                    Text = $"We recommend changing the price for the product {productName} to maximize your profit.",
                    Hyperlink = $"/priceRecommendation/{createdPricePrediction.PricePredictionGUID}",
                    HyperlinkText = "See more details here",
                    Read = false,
                    Date = DateTime.Now,
                    UserGUID = up.UserGUID
                };
                _notificationsService.Add(notification);
            }

            AppendNewPricePredictionsToExistingOnes();
        }

        private void AppendNewPricePredictionsToExistingOnes()
        {
            var baseLines = File.ReadAllLines(_trainDatasetFilePath).ToList();

            var newLines = File.ReadAllLines(_predictDatasetFilePath);
            baseLines.AddRange(newLines.Skip(1));

            File.WriteAllLines(_trainDatasetFilePath, baseLines);
        }
    }
}