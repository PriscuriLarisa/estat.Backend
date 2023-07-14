using eStat.BLL.Core;
using eStat.BLL.Implementations;
using eStat.BLL.Interfaces;
using eStat.BLL.Jobs;
using eStat.BLL.Jobs.Interfaces;
using eStat.BLL.MLLogic;
using eStat.BLL.MLLogic.Interfaces;
using eStat.BLL.MLLogic.MLHelpers;
using eStat.BLL.MLLogic.MLHelpers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eStat.BLL.Configuration
{
    public static class ConfigureBLL
    {
        public static IServiceCollection AddBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<BusinessContext>();
            services.AddScoped<IProducts, ProductsBL>();
            services.AddScoped<IUserProducts, UserProductsBL>();
            services.AddScoped<IOrderProducts, OrderProductsBL>();
            services.AddScoped<IPurchases, PurchasesBL>();
            services.AddScoped<IOrder, OrdersBL>();
            services.AddScoped<IShoppingCarts, ShoppingCartsBL>();
            services.AddScoped<IShoppingCartProducts, ShoppingCartProductsBL>();
            services.AddScoped<IPurchaseProducts, PurchaseProductsBL>();
            services.AddScoped<IUsers, UsersBL>();
            services.AddScoped<IAuthentication, AuthenticationBL>();
            services.AddScoped<IJWT, JWTBL>();
            services.AddScoped<ISearches, SearchesBL>();
            services.AddScoped<INotifications, NotificationsBL>();
            services.AddScoped<IPriceChanges, PriceChangesBL>();
            services.AddScoped<IPricePredictions, PricePredictionsBL>();
            services.AddTransient<INotificationSystemHub, NotificationSystemHub>();
            services.AddTransient<INotificationGenerator, NotificationGenerator>();
            services.AddTransient<IPredicitonDataGenerator, PredictionDataGenerator>();
            services.AddTransient<IPricePredictionHandler, PricePredictionHandler>();
            services.AddHostedService<NotificationScheduledService>();
            services.AddSingleton<NotificationScheduledService>(p => p.GetServices<IHostedService>().OfType<NotificationScheduledService>().Single());
            services.AddHostedService<PricePredictionScheduledService>();
            services.AddSingleton<PricePredictionScheduledService>(p => p.GetServices<IHostedService>().OfType<PricePredictionScheduledService>().Single());
            services.AddLogging();

            return services;
        }
    }
}
