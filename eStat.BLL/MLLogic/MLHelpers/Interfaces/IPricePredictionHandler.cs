namespace eStat.BLL.MLLogic.MLHelpers.Interfaces
{
    public interface IPricePredictionHandler
    {
        void HandlePricePredictions(List<Library.Models.PricePrediction> pricePredictions);
    }
}
