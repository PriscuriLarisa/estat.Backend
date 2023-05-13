namespace eStat.Library.Converters
{
    public static class ShoppingCartProductConverter
    {
        public static Library.Models.ShoppingCartProduct ToDTO(DAL.Entities.ShoppingCartProduct shoppingCartProduct)
        {
            return new Library.Models.ShoppingCartProduct
            {
                ShoppingCartProductGUID = shoppingCartProduct.ShoppingCartProductGUID,
                ShoppingCartGUID = shoppingCartProduct.ShoppingCartGUID,
                UserProductGUID = shoppingCartProduct.UserProductGUID,
                UserProduct = UserProductConverter.ToDTOWithUser(shoppingCartProduct.UserProduct),
                Quantity = shoppingCartProduct.Quantity
            };
        }

        public static DAL.Entities.ShoppingCartProduct ToEntity(Library.Models.ShoppingCartProduct shoppingCartProduct)
        {
            return new DAL.Entities.ShoppingCartProduct
            {
                ShoppingCartProductGUID = shoppingCartProduct.ShoppingCartProductGUID,
                ShoppingCartGUID = shoppingCartProduct.ShoppingCartGUID,
                UserProductGUID = shoppingCartProduct.UserProductGUID,
                UserProduct = null,
                Quantity = shoppingCartProduct.Quantity
            };
        }
    }
}
