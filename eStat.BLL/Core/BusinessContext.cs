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
        public IOrders OrdersBL { get; set; }
        public IOrderProducts OrderProductsBL { get; set; }
        //public ISearches SearchesBL { get; set; }
        public IPurchases PurchasesBL { get; set; }
        public IPurchaseProducts PurchaseProductsBL { get; set; }
        //public IProductRequests ProductRequestsBL { get; set; }

        public BusinessContext(IUsers usersBL, IProducts productsBL, IUserProducts userProductsBL, IOrders ordersBL, IOrderProducts orderProductsBL/*, ISearches searchesBL*/, IPurchases purchasesBL, IPurchaseProducts purchaseProductsBL/*, IProductRequests productRequestsBL*/)
        {
            UsersBL = usersBL;
            ProductsBL = productsBL;
            UserProductsBL = userProductsBL;
            OrdersBL = ordersBL;
            OrderProductsBL = orderProductsBL;
            //SearchesBL = searchesBL;
            PurchasesBL = purchasesBL;
            PurchaseProductsBL = purchaseProductsBL;
            //ProductRequestsBL = productRequestsBL;
        }
    }
}
