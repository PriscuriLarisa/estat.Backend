using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IShoppingCarts
    {
        ShoppingCart GetshoppingCartByUser(Guid userUid);
        List<ShoppingCart> GetAll();
        ShoppingCart GetByUid(Guid uid);
        ShoppingCart Add(ShoppingCartCreate shoppingCart);
        void AddItemToCart(ShoppingCartProductAdd shoppingCartProduct);
    }
}