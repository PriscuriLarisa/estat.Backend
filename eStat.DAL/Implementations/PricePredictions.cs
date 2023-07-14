using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class PricePredictions : DALObject, IPricePredictions
    {
        public PricePredictions(DatabaseContext context) : base(context)
        {
        }

        public PricePrediction Add(PricePrediction pricePrediction)
        {
            EntityEntry<PricePrediction>? addedpricePrediction = _context.PricePredictions.Add(pricePrediction);
            _context.SaveChanges();
            return addedpricePrediction.Entity;
        }

        public List<PricePrediction> GetAll()
        {
            return _context.PricePredictions.ToList();
        }

        public PricePrediction? GetByUid(Guid uid)
        {
            return _context.PricePredictions.FirstOrDefault(n => n.PricePredictionGUID == uid);
        }
    }
}
