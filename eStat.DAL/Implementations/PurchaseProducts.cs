using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class PurchaseProducts : DALObject, IPurchaseProducts
    {
        public PurchaseProducts(DatabaseContext context) : base(context)
        {
        }

        public PurchaseProduct Add(PurchaseProduct purchaseProduct)
        {
            EntityEntry<PurchaseProduct>? addedPurchaseProduct = _context.PurchaseProducts.Add(purchaseProduct);
            _context.SaveChanges();
            return addedPurchaseProduct.Entity;
        }

        public void Delete(Guid uid)
        {
            PurchaseProduct purchaseProduct = _context.PurchaseProducts.FirstOrDefault(op => op.PurchaseProductGUID == uid);
            if (purchaseProduct == null)
                return;

            _context.PurchaseProducts.Remove(purchaseProduct);
            _context.SaveChanges();
        }

        public List<PurchaseProduct> GetAll()
        {
            return _context.PurchaseProducts
               .Include(op => op.UserProduct)
               .Include(op => op.Purchase)
               .ToList();
        }

        public PurchaseProduct? GetByUid(Guid uid)
        {
            return _context.PurchaseProducts
                .Include(op => op.UserProduct)
                .Include(op => op.Purchase)
                .FirstOrDefault(op => op.PurchaseProductGUID == uid);
        }

        public List<PurchaseProduct> GetByPurchase(Guid purchaseUid)
        {
            return _context.PurchaseProducts
                .Include(op => op.UserProduct)
                .Include(op => op.Purchase)
                .Where(op => op.PurchaseGUID == purchaseUid)
                .ToList();
        }

        public List<PurchaseProduct> GetByUser(Guid userUid)
        {
            return _context.PurchaseProducts
                .Include(op => op.UserProduct)
                .Include(op => op.Purchase)
                .Where(op => op.Purchase.UserGUID == userUid)
                .ToList();
        }

        public void Update(PurchaseProduct purchaseProduct)
        {
            PurchaseProduct oldPurchaseProduct = _context.PurchaseProducts.FirstOrDefault(op => op.PurchaseProductGUID == purchaseProduct.PurchaseProductGUID);
            if (oldPurchaseProduct == null) return;
            oldPurchaseProduct.UserProductGUID = purchaseProduct.UserProductGUID;
            oldPurchaseProduct.PurchaseGUID = purchaseProduct.PurchaseGUID;
            oldPurchaseProduct.Price = purchaseProduct.Price;

            _context.PurchaseProducts.Update(oldPurchaseProduct);
            _context.SaveChanges();
        }
    }
}
