using eStat.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStat.BLL.Core
{
    public class BusinessContext
    {
        public IUsers UsersBL { get; set; }
        public IProducts ProductsBL { get; set; }
        public IUserProducts UserProductsBL { get; set; }
        public IShoppingCarts ShoppingCartsBL { get; set; }
        public IOrder OrdersBL { get; set; }
        public IOrderProducts OrderProductsBL { get; set; }
        public ISearches SearchesBL { get; set; }
        public IPurchases PurchasesBL { get; set; }
        public IPurchaseProducts PurchaseProductsBL { get; set; }
        public IShoppingCartProducts ShoppingCartProductsBL { get; set; }
        public IAuthentication AuthenticationBL { get; set; }
        public IJWT JWTBL { get; set; }
        public INotifications NotificationsBL { get; set; }
        public IPriceChanges PriceChangesBL { get; set; }
        public IPricePredictions PricePredictionsBL { get; set; }
        //public IProductRequests ProductRequestsBL { get; set; }

        public BusinessContext(IUsers usersBL, 
            IProducts productsBL,
            IUserProducts userProductsBL,
            IOrder ordersBL,
            IOrderProducts orderProductsBL,
            IPurchases purchasesBL,
            IPurchaseProducts purchaseProductsBL/*, IProductRequests productRequestsBL*/,
            IShoppingCarts shoppingCartsBL,
            IShoppingCartProducts shoppingCartProducts,
            IAuthentication authentication,
            IJWT jwt,
            ISearches searchesBL,
            INotifications notifications,
            IPriceChanges priceChangesBL,
            IPricePredictions pricePredictionsBL)
        {
            UsersBL = usersBL;
            ProductsBL = productsBL;
            UserProductsBL = userProductsBL;
            OrdersBL = ordersBL;
            OrderProductsBL = orderProductsBL;
            SearchesBL = searchesBL;
            PurchasesBL = purchasesBL;
            PurchaseProductsBL = purchaseProductsBL;
            ShoppingCartsBL = shoppingCartsBL;
            ShoppingCartProductsBL = shoppingCartProducts;
            AuthenticationBL = authentication;
            JWTBL = jwt;
            NotificationsBL = notifications;
            PriceChangesBL = priceChangesBL;
            PricePredictionsBL = pricePredictionsBL;
            //ProductRequestsBL = productRequestsBL;
        }
    }
}
