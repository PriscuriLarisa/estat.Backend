using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;

namespace eStat.BLL.Implementations
{
    public class PricePredictionsBL : BusinessObject, IPricePredictions
    {
        public PricePredictionsBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public PricePrediction Add(PricePrediction order)
        {
            return PricePredictionConverter.ToDTO(_dalContext.PricePredictions.Add(PricePredictionConverter.ToEntity(order)));
        }

        public List<PricePrediction> GetAll()
        {
            return _dalContext.PricePredictions.GetAll().Select(x => PricePredictionConverter.ToDTO(x)).ToList();
        }

        public PricePrediction? GetByUid(Guid uid)
        {
            var returnedPricePrediction = _dalContext.PricePredictions.GetByUid(uid);
            if(returnedPricePrediction != null)
            {
                return PricePredictionConverter.ToDTO(returnedPricePrediction);
            }
            return null;
        }
    }
}
