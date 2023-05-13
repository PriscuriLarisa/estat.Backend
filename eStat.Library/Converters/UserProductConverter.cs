namespace eStat.Library.Converters
{
    public static class UserProductConverter
    {
        public static Library.Models.UserProduct ToDTO(DAL.Entities.UserProduct userProduct)
        {
            return new Models.UserProduct
            {
                UserProductGUID= userProduct.UserProductGUID,
                ProductGUID = userProduct.ProductGUID,
                Price = userProduct.Price,
                Product = ProductConverter.ToDTO(userProduct.Product),
                UserGUID = userProduct.UserGUID,
                Quantity = userProduct.Quantity
            };
        }

        public static Library.Models.UserProduct ToDTOWithUser(DAL.Entities.UserProduct userProduct)
        {
            return new Models.UserProduct
            {
                UserProductGUID = userProduct.UserProductGUID,
                ProductGUID = userProduct.ProductGUID,
                Price = userProduct.Price,
                Product = ProductConverter.ToDTO(userProduct.Product),
                UserGUID = userProduct.UserGUID,
                Quantity = userProduct.Quantity,
                User = UserConverter.ToDTOInfo(userProduct.User)
            };
        }

        public static DAL.Entities.UserProduct ToEntity(Library.Models.UserProduct userProduct)
        {
            return new DAL.Entities.UserProduct
            {
                UserProductGUID = userProduct.UserProductGUID,
                ProductGUID = userProduct.ProductGUID,
                UserGUID = userProduct.UserGUID,
                Price = userProduct.Price,
                Quantity = userProduct.Quantity,
            };
        }
    }
}
