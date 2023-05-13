namespace eStat.Library.Converters
{
    public static class ShoppingCartConverter
    {
        public static Library.Models.ShoppingCart ToDTO(DAL.Entities.ShoppingCart shoppingCart)
        {
            return new Library.Models.ShoppingCart
            {
                ShoppingCartGUID = shoppingCart.ShoppingCartGUID,
                UserGUID = shoppingCart.UserGUID,
                User = UserConverter.ToDTOInfo(shoppingCart.User),
                Products = shoppingCart.Products.Select(p => ShoppingCartProductConverter.ToDTO(p)).ToList(),
            };
        }

        public static DAL.Entities.ShoppingCart ToEntity(Library.Models.ShoppingCart shoppingCart)
        {
            return new DAL.Entities.ShoppingCart
            {
                ShoppingCartGUID = shoppingCart.ShoppingCartGUID,
                UserGUID = shoppingCart.UserGUID,
                Products = shoppingCart.Products.Select(p => ShoppingCartProductConverter.ToEntity(p)).ToList(),
            };
        }
    }
}
