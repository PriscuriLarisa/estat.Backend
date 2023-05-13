using eStat.BLL.Core;
using eStat.BLL.Implementations;
using eStat.BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }
    }
}
