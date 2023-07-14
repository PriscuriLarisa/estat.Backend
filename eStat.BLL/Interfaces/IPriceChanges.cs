using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IPriceChanges
    {
        List<PriceChange> GetAll();
        PriceChange? GetByUid(Guid uid);
        PriceChange Add(PriceChangeCreate pc);
        PriceChange AddWithDate(PriceChangeCreate pc, DateTime date);
        void Update(PriceChange pc);
        List<PriceChange> GetByUser(Guid userGuid, Guid productGuid);
        List<PriceChange> GetByUserInLastMonth(Guid userGuid, Guid productGuid);
        List<PriceChange> GetByProduct(Guid productGuid);
        List<PriceChange> GetByProductInLastMonth(Guid productGuid);
        List<PriceChange> GetFromLastWeek();
        decimal GetAveragePriceLastMonthByUserProduct(Guid userProductGuid);
        Dictionary<Guid, decimal> GetAveragePricesLastMonthByUserProducts(List<Guid> userProductGuids);
        Dictionary<Guid, decimal> GetAveragePricesLastMonthByProducts(List<Guid> productGuids);

    }
}