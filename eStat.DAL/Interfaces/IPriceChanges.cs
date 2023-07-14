using eStat.DAL.Entities;

namespace eStat.DAL.Interfaces
{
    public interface IPriceChanges
    {
        PriceChange Add(PriceChange priceChange);
        void Update(PriceChange priceChange);
        List<PriceChange> GetAll();
        PriceChange? GetByUid(Guid uid);
    }
}