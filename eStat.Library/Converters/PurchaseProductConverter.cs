namespace eStat.Library.Converters
{
    public static class PurchaseProductConverter
    {
        public static Library.Models.PurchaseProduct ToDTO(DAL.Entities.PurchaseProduct purchaseProduct)
        {
            return new Library.Models.PurchaseProduct
            {
                PurchaseProductGUID = purchaseProduct.PurchaseProductGUID,
                PurchaseGUID = purchaseProduct.PurchaseGUID,
                UserProductGUID = purchaseProduct.UserProductGUID,
                Product = UserProductConverter.ToDTOWithUser(purchaseProduct.UserProduct),
                Price = purchaseProduct.Price,
                Quantity = purchaseProduct.Quantity
            };
        }

        public static DAL.Entities.PurchaseProduct ToEntity(Library.Models.PurchaseProduct purchaseProduct)
        {
            return new DAL.Entities.PurchaseProduct
            {
                PurchaseProductGUID = purchaseProduct.PurchaseProductGUID,
                PurchaseGUID = purchaseProduct.PurchaseGUID,
                UserProductGUID = purchaseProduct.UserProductGUID,
                //Product = UserProductConverter.ToEntity(purchaseProduct.Product),
                Price = purchaseProduct.Price,
                Quantity = purchaseProduct.Quantity
            };
        }
    }
}
