using eStat.DAL.Core.Context.Interfaces;
using eStat.DAL.Interfaces;

namespace eStat.DAL.Core.Context
{
    public class DALContext : IDALContext
    {
        public IUsers Users { get; set; }
        public IProducts Products { get; set; }
        public IUserProducts UserProducts { get; set; }
        public ISearches Searches { get; set; }
        //public IProductRequests ProductRequests { get; set; }
        public IOrderProducts OrderProducts { get; set; }
        public IOrders Orders { get; set; }
        public IPurchaseProducts PurchaseProducts { get; set; }
        public IPurchases Purchases { get; set; }
        public IShoppingCarts ShoppingCarts { get; set; }
        public IShoppingCartProducts ShoppingCartProducts { get; set; }
        public INotifications Notifications { get; set; }
        public IPriceChanges PriceChanges { get; set; }
        public IPricePredictions PricePredictions { get; set; }


        public DALContext(IUsers users,
            IProducts products,
            IUserProducts userProducts/*, ISearches searches, IProductRequests productRequests*/,
            IOrderProducts orderProducts,
            IOrders orders,
            IPurchaseProducts purchaseProducts,
            IPurchases purchases,
            IShoppingCarts shoppingCarts,
            IShoppingCartProducts shoppingCartProducts,
            ISearches searches,
            INotifications notifications,
            IPriceChanges priceChanges,
            IPricePredictions pricePredictions)
        {
            Users = users;
            Products = products;
            UserProducts = userProducts;
            //Searches = searches;
            //ProductRequests = productRequests;
            OrderProducts = orderProducts;
            Orders = orders;
            PurchaseProducts = purchaseProducts;
            Purchases = purchases;
            ShoppingCarts = shoppingCarts;
            ShoppingCartProducts = shoppingCartProducts;
            Searches = searches;
            Notifications = notifications;
            PriceChanges = priceChanges;
            PricePredictions = pricePredictions;
        }
    }
}
