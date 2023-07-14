using eStat.DAL.Interfaces;

namespace eStat.DAL.Core.Context.Interfaces
{
    public interface IDALContext
    {
        public IUsers Users { get; set; }
        public IProducts Products { get; set; }
        public IUserProducts UserProducts { get; set; }
        //public ISearches Searches { get; set; }
        //public IProductRequests ProductRequests { get; set; }
        public IOrderProducts OrderProducts { get; set; }
        public IOrders Orders { get; set; }
        public IPurchaseProducts PurchaseProducts { get; set; }
        public IPurchases Purchases { get; set; }
        public IShoppingCarts ShoppingCarts { get; set; }
        public IShoppingCartProducts ShoppingCartProducts { get; set; }
        public ISearches Searches { get; set; }
        public INotifications Notifications { get; set; }
        public IPriceChanges PriceChanges { get; set; }
        public IPricePredictions PricePredictions { get; set; }
    }
}