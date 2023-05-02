namespace eStat.Library.Converters
{
    public static class OrderProductConverter
    {
        public static Library.Models.OrderProduct ToDTO(DAL.Entities.OrderProduct orderProduct)
        {
            return new Library.Models.OrderProduct
            {
                OrderProductGUID = orderProduct.OrderProductGUID,
                OrderGUID = orderProduct.OrderGUID,
                UserProductGUID = orderProduct.ProductGUID,
                Product = ProductConverter.ToDTO(orderProduct.Product),
                Price = orderProduct.Price
            };
        }

        public static DAL.Entities.OrderProduct ToEntity(Library.Models.OrderProduct orderProduct)
        {
            return new DAL.Entities.OrderProduct
            {
                OrderProductGUID = orderProduct.OrderProductGUID,
                OrderGUID = orderProduct.OrderGUID,
                ProductGUID = orderProduct.UserProductGUID,
                //Product = ProductConverter.ToEntity(orderProduct.Product),
                Price = orderProduct.Price
            };
        }
    }
}
