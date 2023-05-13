using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;

namespace eStat.DAL.Implementations
{
    public class ShoppingCartProducts : DALObject, IShoppingCartProducts
    {
        public ShoppingCartProducts(DatabaseContext context) : base(context)
        {
        }

        public void Delete(Guid uid)
        {
            ShoppingCartProduct shoppingCart = _context.ShoppingCartProducts.FirstOrDefault(x => x.ShoppingCartProductGUID == uid);
            if (shoppingCart == null)
                return;
            _context.ShoppingCartProducts.Remove(shoppingCart);
            _context.SaveChanges();
        }
    }
}