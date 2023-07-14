using eStat.DAL.Core.Context.Interfaces;
using eStat.DAL.Implementations;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eStat.DAL.Core.Context
{
    public static class ConfigureDAL
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(
                    b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName));
            });

            services.AddScoped<DatabaseContext>();
            services.AddScoped<IProducts, Products>();
            services.AddScoped<IUserProducts, UserProducts>();
            services.AddScoped<IOrderProducts, OrderProducts>();
            services.AddScoped<IOrders, Orders>();
            services.AddScoped<IShoppingCarts, ShoppingCarts>();
            services.AddScoped<IShoppingCartProducts, ShoppingCartProducts>();
            services.AddScoped<IPurchaseProducts, PurchaseProducts>();
            services.AddScoped<IPurchases, Purchases>();
            //services.AddScoped<IProductRequests, ProductRequests>();
            services.AddScoped<ISearches, Searches>();
            services.AddScoped<INotifications, Notifications>();
            services.AddScoped<IUsers, Users>();
            services.AddScoped<IPriceChanges, PriceChanges>();
            services.AddScoped<IPricePredictions, PricePredictions>();
            services.AddScoped<IDALContext, DALContext>();

            return services;
        }
    }
}
