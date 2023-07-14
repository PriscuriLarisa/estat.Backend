using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IUserProducts
    {
        List<UserProduct> GetAll();
        UserProduct? GetByUid(Guid uid);
        UserProduct Add(UserProduct userProduct);
        void Update(UserProduct userProduct);
        void Delete(Guid uid);
        List<UserProduct> GetUserProductsByUser(Guid userUid);
        List<UserProduct> GetUserProductsByProduct(Guid productUid);
        List<UserProduct> GetUserProductsByProductInBatches(Guid userUid, int batchNb, List<string> keyWords);
        Dictionary<int, decimal> GetAvgPriceLastSixMonthsByUserProduct(Guid uid);
        Dictionary<int, decimal> GetMySellsLastSixMonths(Guid uid);
    }
}