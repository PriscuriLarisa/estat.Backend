namespace eStat.Library.Converters
{
    public class PriceChangeConverter
    {
        public static Library.Models.PriceChange ToDTO(DAL.Entities.PriceChange pc)
        {
            return new Library.Models.PriceChange
            {
                PriceChangeGUID = pc.PriceChangeGUID,
                UserProductGUID = pc.UserProductGUID,
                Date = pc.Date,
                ToPrice = pc.ToPrice,
                FromPrice = pc.FromPrice,
                ProductGUID = pc.ProductGUID,
                UserProduct = UserProductConverter.ToDTOWithUser(pc.UserProduct),
                Product = ProductConverter.ToDTO(pc.Product),
            };
        }

        public static DAL.Entities.PriceChange ToEntity(Library.Models.PriceChange pc)
        {
            return new DAL.Entities.PriceChange
            {
                PriceChangeGUID = pc.PriceChangeGUID,
                UserProductGUID = pc.UserProductGUID,
                Date = pc.Date,
                ToPrice= pc.ToPrice,
                FromPrice= pc.FromPrice,
                ProductGUID= pc.ProductGUID,
                //Products = order.Products.Select(p => OrderProductConverter.ToEntity(p)).ToList()
            };
        }

        public static DAL.Entities.PriceChange ToEntity(Library.Models.PriceChangeCreate pc)
        {
            return new DAL.Entities.PriceChange
            {
                PriceChangeGUID = Guid.Empty,
                UserProductGUID = pc.UserProductGUID,
                Date = DateTime.Now,
                ToPrice = pc.ToPrice,
                FromPrice = pc.FromPrice,
                ProductGUID = pc.ProductGUID,
                //Products = order.Products.Select(p => OrderProductConverter.ToEntity(p)).ToList()
            };
        }
    }
}