using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class PriceChanges : DALObject, IPriceChanges
    {
        public PriceChanges(DatabaseContext context) : base(context)
        {
        }

        public PriceChange Add(PriceChange priceChange)
        {
            EntityEntry<PriceChange>? addedPriceChange = _context.PriceChanges.Add(priceChange);
            _context.SaveChanges();
            if (addedPriceChange == null || addedPriceChange.Entity.PriceChangeGUID == null)
                return null;
            PriceChange createdPriceChange = GetByUid(addedPriceChange.Entity.PriceChangeGUID);
            return createdPriceChange;
        }

        public List<PriceChange> GetAll()
        {
            return _context.PriceChanges
                .Include(pc => pc.UserProduct)
                    .ThenInclude(p => p.Product)
                .Include(pc => pc.UserProduct)
                    .ThenInclude(p => p.User)
                .Include(pc => pc.Product)
                .ToList();
        }

        public PriceChange? GetByUid(Guid uid)
        {
            return _context.PriceChanges
                .Include(pc => pc.UserProduct)
                    .ThenInclude(p => p.Product)
                .Include(pc => pc.UserProduct)
                    .ThenInclude(p => p.User)
                .Include(pc => pc.Product)
                .FirstOrDefault(pc => pc.PriceChangeGUID == uid);
        }

        public void Update(PriceChange priceChange)
        {
            PriceChange oldPriceChange = GetByUid(priceChange.PriceChangeGUID);
            if (oldPriceChange == null) {
                return;
            }

            oldPriceChange.FromPrice = priceChange.FromPrice;
            oldPriceChange.ToPrice = priceChange.ToPrice;
            oldPriceChange.UserProductGUID = priceChange.UserProductGUID;
            oldPriceChange.ProductGUID = priceChange.ProductGUID;

            _context.PriceChanges.Update(oldPriceChange);
            _context.SaveChanges();
        }
    }
}
