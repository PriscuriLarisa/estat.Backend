using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class Purchases : DALObject, IPurchases
    {
        public Purchases(DatabaseContext context) : base(context)
        {
        }

        public Purchase Add(Purchase purchase)
        {
            EntityEntry<Purchase>? addedPurchase = _context.Purchases.Add(purchase);
            _context.SaveChanges();
            return addedPurchase.Entity;
        }

        public void Delete(Guid uid)
        {
            DeletePurchase(uid);
            _context.SaveChanges();
        }

        public List<Purchase> GetAll()
        {
            return _context.Purchases
                .Include(p => p.Products)
                    .ThenInclude(pp => pp.UserProduct)
                .Include(p => p.User)
                .ToList();
        }

        public Purchase? GetByUid(Guid uid)
        {
            return _context.Purchases
                .Include(p => p.Products)
                .ThenInclude(pp => pp.UserProduct)
                .Include(p => p.User)
                .FirstOrDefault(user => user.UserGUID == uid);
        }

        public List<Purchase> GetPurchasesByUser(Guid userUid)
        {
            return _context.Purchases
                .Include(s => s.Products)
                    .ThenInclude(p => p.UserProduct)
                    .ThenInclude(up => up.Product)
                .Include(s => s.Products)
                    .ThenInclude(p => p.UserProduct)
                    .ThenInclude(up => up.User)
                .Where(user => user.UserGUID == userUid)
                .ToList();
        }

        public void Update(Purchase purchase)
        {
            Purchase oldPurchase = _context.Purchases.FirstOrDefault(o => o.PurchaseGUID == purchase.PurchaseGUID);
            if (oldPurchase == null) return;
            oldPurchase.UserGUID = purchase.UserGUID;
            oldPurchase.Products = purchase.Products;

            _context.Purchases.Update(oldPurchase);
            _context.SaveChanges();
        }

        private void DeletePurchase(Guid uid)
        {
            Purchase purchase = _context.Purchases.Include(order => order.Products).FirstOrDefault(order => order.UserGUID == uid);
            if (purchase == null)
                return;

            foreach (PurchaseProduct orderProduct in purchase.Products)
            {
                _context.Entry(orderProduct).State = EntityState.Deleted;
            }
            _context.Purchases.Remove(purchase);
        }
    }
}