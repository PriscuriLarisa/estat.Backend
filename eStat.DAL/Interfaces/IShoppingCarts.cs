using eStat.DAL.Entities;

namespace eStat.DAL.Interfaces
{
    public interface IShoppingCarts
    {
        List<ShoppingCart> GetAll();
        ShoppingCart? GetByUid(Guid uid);
        ShoppingCart Add(ShoppingCart shoppingCart);
        void Update(ShoppingCart shoppingCart);
        void Delete(Guid uid);
        ShoppingCart GetShoppingCartByUser(Guid userUid);
    }
}