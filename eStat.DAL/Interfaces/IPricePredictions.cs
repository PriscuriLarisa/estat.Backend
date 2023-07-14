using eStat.DAL.Entities;

namespace eStat.DAL.Interfaces
{
    public interface IPricePredictions
    {
        List<PricePrediction> GetAll();
        PricePrediction? GetByUid(Guid uid);
        PricePrediction Add(PricePrediction assignment);
    }
}
