using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class UserProducts : DALObject, IUserProducts
    {
        public UserProducts(DatabaseContext context) : base(context)
        {
        }

        public UserProduct Add(UserProduct userProduct)
        {
            EntityEntry<UserProduct>? addedUserProduct = _context.UserProducts.Add(userProduct);
            _context.SaveChanges();
            return addedUserProduct.Entity;
        }

        public void Delete(Guid uid)
        {
            UserProduct userProduct = _context.UserProducts.FirstOrDefault(userProduct => userProduct.UserProductGUID == uid);
            if (userProduct == null)
                return;
            _context.UserProducts.Remove(userProduct);
            _context.SaveChanges();
        }

        public List<UserProduct> GetAll()
        {
            return _context.UserProducts
            .Include(up => up.Product)
            .Include(up => up.User)
            .ToList();
        }

        public UserProduct? GetByUid(Guid uid)
        {
            return _context.UserProducts
                .Include(up => up.Product)
                .Include(up => up.User)
                .FirstOrDefault(up => up.UserProductGUID == uid);
        }

        public List<UserProduct> GetUserProductsByProduct(Guid productUid)
        {
            return _context.UserProducts
                .Include(up => up.Product)
                .Include(up => up.User)
                .Where(up => up.ProductGUID == productUid)
                .ToList();
        }

        public List<UserProduct> GetUserProductsByUser(Guid userUid)
        {
            return _context.UserProducts
                .Include(up => up.Product)
                .Include(up => up.User)
                .Where(up => up.UserGUID == userUid)
                .ToList();
        }

        public void Update(UserProduct userProduct)
        {
            UserProduct oldUserProduct = _context.UserProducts.FirstOrDefault(u => u.UserGUID == userProduct.UserGUID);
            if (oldUserProduct == null) return;
            oldUserProduct.Price = userProduct.Price;
            oldUserProduct.Quantity = userProduct.Quantity;
            oldUserProduct.UserGUID = userProduct.UserGUID;
            oldUserProduct.ProductGUID = userProduct.ProductGUID;
            

            _context.UserProducts.Update(oldUserProduct);
            _context.SaveChanges();
        }
    }
}
