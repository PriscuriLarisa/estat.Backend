using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class ShoppingCarts : DALObject, IShoppingCarts
    {
        public ShoppingCarts(DatabaseContext context) : base(context)
        {
        }

        public ShoppingCart Add(ShoppingCart shoppingCart)
        {
            EntityEntry<ShoppingCart>? addedShoppingCart = _context.ShoppingCarts.Add(shoppingCart);
            _context.SaveChanges();
            return addedShoppingCart.Entity;
        }

        public void Delete(Guid uid)
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCart> GetAll()
        {
            return _context.ShoppingCarts
                .Include(s => s.Products)
                    .ThenInclude(p => p.UserProduct)
                    .ThenInclude(p => p!.Quantity)
                .Include(s => s.User)
                .ToList();
        }

        public ShoppingCart? GetByUid(Guid uid)
        {
            throw new NotImplementedException();
        }

        public ShoppingCart GetShoppingCartByUser(Guid userUid)
        {
            return _context.ShoppingCarts
                .Include(s => s.Products)
                    .ThenInclude(p => p.UserProduct)
                    .ThenInclude(p => p!.Quantity)
                .Include(s => s.User)
                .FirstOrDefault(s => s.UserGUID == userUid);
        }

        public void Update(ShoppingCart shoppingCart)
        {
            ShoppingCart oldShoppingCart = _context.ShoppingCarts.FirstOrDefault(s => s.ShoppingCartGUID == shoppingCart.ShoppingCartGUID);
            if (oldShoppingCart == null) return;
            oldShoppingCart.UserGUID = oldShoppingCart.UserGUID;
            oldShoppingCart.Products = oldShoppingCart.Products;

            _context.ShoppingCarts.Update(oldShoppingCart);
            _context.SaveChanges();
        }
    }
}