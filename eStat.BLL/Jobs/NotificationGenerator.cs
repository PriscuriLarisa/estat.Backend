using eStat.BLL.Interfaces;
using eStat.BLL.Jobs.Interfaces;
using eStat.Library.Models;
using Microsoft.Extensions.DependencyInjection;

namespace eStat.BLL.Jobs
{
    public class NotificationGenerator : INotificationGenerator
    {
        private readonly INotifications _notificationsService;
        private readonly IPriceChanges _priceChangesService;
        private readonly ISearches _searchesService;

        public NotificationGenerator(IServiceScopeFactory factory)
        {
            _notificationsService = factory.CreateScope().ServiceProvider.GetRequiredService<INotifications>();
            _priceChangesService = factory.CreateScope().ServiceProvider.GetRequiredService<IPriceChanges>();
            _searchesService = factory.CreateScope().ServiceProvider.GetRequiredService<ISearches>();
        }

        public Dictionary<string, string> GenerateNotificationForPriceChanges()
        {
            Dictionary<string, string> notifications = new Dictionary<string, string>();

            List<PriceChange> priceChangesFromLastWeek = _priceChangesService.GetFromLastWeek();
            List<Guid> userProductGuidsWithPriceChanges = priceChangesFromLastWeek.Where(p => p.UserProductGUID != Guid.Empty)
                                                                                .Select(p => p.UserProductGUID)
                                                                                .Distinct()
                                                                                .ToList();


            foreach (Guid userProductGuidWithPriceChanges in userProductGuidsWithPriceChanges)
            {
                List<PriceChange> priceChanges = priceChangesFromLastWeek.Where(p => p.UserProductGUID == userProductGuidWithPriceChanges)
                                                                        .OrderBy(p => p.Date)
                                                                        .ToList();
                Guid productGuid = priceChanges.First().ProductGUID;
                string retailer = $"{priceChanges.First().UserProduct.User.FirstName} {priceChanges.First().UserProduct.User.LastName}";
                decimal fromPrice = priceChanges.First().FromPrice;
                decimal toPrice = priceChanges.Last().ToPrice;
                decimal percentage = Math.Round((((fromPrice - toPrice) / fromPrice) * 100), 0);
                if (toPrice >= fromPrice)
                    continue;

                List<Search> searches = _searchesService.GetByProductFromLastMonth(productGuid);
                Dictionary<Guid, int> results = searches.GroupBy(s => s.UserGUID,
                                                                s => s.ProductGUID,
                                                                (key, value) => new { key = key, value = value.Count() })
                    .Where(g => g.value >= 10)
                    .ToDictionary(g => g.key, g => g.value);

                foreach(KeyValuePair<Guid, int> kvp in results)
                {
                    Notification notif = new Notification
                    {
                        NotificationGUID = Guid.Empty,
                        Title = "Price discount",
                        Text = $"The retailer {retailer} has discounted the following product's price by {percentage}% in the last week.",
                        Read = false,
                        Hyperlink = $"/productPage/{productGuid}",
                        HyperlinkText = "Check it out",
                        UserGUID = kvp.Key,
                        Date = DateTime.Now,
                    };
                    _notificationsService.Add(notif);
                    notifications.Add(kvp.Key.ToString().ToLowerInvariant(), "notify");
                }
            }

            return notifications;
        }
    }
}
