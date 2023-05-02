namespace eStat.Library.Converters
{
    public static class PurchaseConverter
    {
        public static Library.Models.Purchase ToDTO(DAL.Entities.Purchase purchase)
        {
            return new Library.Models.Purchase
            {
                PurchaseGUID = purchase.PurchaseGUID,
                UserGUID = purchase.UserGUID,
                Date = purchase.Date,
                Products =  purchase.Products.Select(p => PurchaseProductConverter.ToDTO(p)).ToList()
            };
        }

        public static DAL.Entities.Purchase ToEntity(Library.Models.Purchase purchase)
        {
            return new DAL.Entities.Purchase
            {
                PurchaseGUID = purchase.PurchaseGUID,
                UserGUID = purchase.UserGUID,
                Date = purchase.Date,
                //Products = purchase.Products.Select(p => PurchaseProductConverter.ToEntity(p)).ToList()
            };
        }
    }
}
