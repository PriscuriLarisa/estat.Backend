using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStat.BLL.Implementations
{
    public class ShoppingCartProductsBL : BusinessObject, IShoppingCartProducts
    {
        public ShoppingCartProductsBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public void Delete(Guid uid)
        {
            _dalContext.ShoppingCartProducts.Delete(uid);
        }
    }
}
