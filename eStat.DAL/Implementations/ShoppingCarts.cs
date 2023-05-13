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
            return _context.ShoppingCarts
                .Include(s => s.Products)
                .Include(s => s.User)
                .FirstOrDefault(p => p.ShoppingCartGUID == addedShoppingCart.Entity.ShoppingCartGUID);
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
                    .ThenInclude(up => up.Product)
                .Include(s => s.Products)
                    .ThenInclude(p => p.UserProduct)
                    .ThenInclude(up => up.User)
                .Include(s => s.User)
                .ToList();
        }

        public ShoppingCart? GetByUid(Guid uid)
        {
            return _context.ShoppingCarts
                .Include(s => s.Products)
                    .ThenInclude(p => p.UserProduct)
                    .ThenInclude(up => up.Product)
                .Include(s => s.Products)
                    .ThenInclude(p => p.UserProduct)
                    .ThenInclude(up => up.User)
                .Include(s => s.User)
                .FirstOrDefault(s => s.ShoppingCartGUID == uid);
        }

        public ShoppingCart GetShoppingCartByUser(Guid userUid)
        {
            return _context.ShoppingCarts
                .Include(s => s.Products)
                    .ThenInclude(p => p.UserProduct)
                    .ThenInclude(up => up.Product)
                .Include(s => s.Products)
                    .ThenInclude(p => p.UserProduct)
                    .ThenInclude(up => up.User)
                .Include(s => s.User)
                .FirstOrDefault(s => s.UserGUID == userUid);
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _context.ChangeTracker.Clear();
            ShoppingCart oldShoppingCart = _context.ShoppingCarts.FirstOrDefault(s => s.ShoppingCartGUID == shoppingCart.ShoppingCartGUID);
            if (oldShoppingCart == null) return;
            oldShoppingCart.Products = shoppingCart.Products;

            _context.ShoppingCarts.Update(shoppingCart);
            _context.SaveChanges();
        }

        public void AddProduct(ShoppingCartProduct shoppingCartProduct)
        {
            _context.ChangeTracker.Clear();
            ShoppingCart oldShoppingCart = GetByUid(shoppingCartProduct.ShoppingCartGUID ?? Guid.Empty);
            if (oldShoppingCart == null) return;
            ShoppingCartProduct existingShoppingCartProduct = oldShoppingCart.Products.FirstOrDefault(p => p.UserProductGUID == shoppingCartProduct.UserProductGUID);
            if(existingShoppingCartProduct == null)
            {
                oldShoppingCart.Products.Add(shoppingCartProduct);
            }
            else
            {
                existingShoppingCartProduct.Quantity += shoppingCartProduct.Quantity;
            }

            _context.ShoppingCarts.Update(oldShoppingCart);
            _context.SaveChanges();
        }
    }
}