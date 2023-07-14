using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IPricePredictions
    {
        List<PricePrediction> GetAll();
        PricePrediction? GetByUid(Guid uid);
        PricePrediction Add(PricePrediction order);
    }
}
