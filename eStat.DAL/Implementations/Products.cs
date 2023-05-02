using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class Products : DALObject, IProducts
    {
        public Products(DatabaseContext context) : base(context)
        {
        }

        public Product Add(Product product)
        {
            EntityEntry<Product>? addedProduct = _context.Products.Add(product);
            _context.SaveChanges();
            return addedProduct.Entity;
        }

        //TODO implement good way to delete
        public void Delete(Guid uid)
        {
            Product oldProduct = _context.Products.FirstOrDefault(p => p.ProductGUID == uid);
            if (oldProduct == null) return;
            oldProduct.InUse = false;
            _context.Products.Update(oldProduct);
            _context.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return _context.Products
                .ToList();
        }

        public Product? GetByUid(Guid uid)
        {
            return _context.Products
                .FirstOrDefault(p => p.ProductGUID == uid);
        }

        public List<string> GetProductsCategories()
        {
            return _context.Products.Select(p => p.Category).Distinct().ToList();
        }

        public void PutInUse(Guid productGuid)
        {
            Product oldProduct = _context.Products.FirstOrDefault(p => p.ProductGUID == productGuid);
            if (oldProduct == null) return;
            oldProduct.InUse = true;

            _context.Products.Update(oldProduct);
            _context.SaveChanges();
        }

        public void PutOutOfUse(Guid productGuid)
        {
            Product oldProduct = _context.Products.FirstOrDefault(p => p.ProductGUID == productGuid);
            if (oldProduct == null) return;
            oldProduct.InUse = false;

            _context.Products.Update(oldProduct);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            Product oldProduct = _context.Products.FirstOrDefault(p => p.ProductGUID == product.ProductGUID);
            if (oldProduct == null) return;
            oldProduct.Characteristics = product.Characteristics;
            oldProduct.InUse = product.InUse;
            oldProduct.BasePrice = product.BasePrice;
            oldProduct.Name = product.Name;
            oldProduct.Category= product.Category;

            _context.Products.Update(oldProduct);
            _context.SaveChanges();
        }
    }
}
