namespace eStat.Library.Converters
{
    public static class OrderConverter
    {
        public static Library.Models.Order ToDTO(DAL.Entities.Order order)
        {
            return new Library.Models.Order
            {
                OrderGUID = order.OrderGUID,
                UserGUID = order.UserGUID,
                Date = order.Date,
                Address = order.Address,
                Products = order.Products.Select(p => OrderProductConverter.ToDTO(p)).ToList()
            };
        }

        public static DAL.Entities.Order ToEntity(Library.Models.Order order)
        {
            return new DAL.Entities.Order
            {
                OrderGUID = order.OrderGUID,
                UserGUID = order.UserGUID,
                Date = order.Date,
                Address = order.Address,
                //Products = order.Products.Select(p => OrderProductConverter.ToEntity(p)).ToList()
            };
        }
    }
}
