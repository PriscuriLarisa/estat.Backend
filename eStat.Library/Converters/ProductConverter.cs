namespace eStat.Library.Converters
{
    public static class ProductConverter
    {
        public static Library.Models.Product ToDTO(DAL.Entities.Product product)
        {
            return new Library.Models.Product
            {
                ProductGUID = product.ProductGUID,
                Characteristics = product.Characteristics,
                InUse = product.InUse,
                Category= product.Category,
                Name= product.Name,
                ImageLink= product.ImageLink,
                BasePrice= product.BasePrice,
            };
        }

        public static DAL.Entities.Product ToEntity(Library.Models.Product product)
        {
            return new DAL.Entities.Product
            {
                ProductGUID = product.ProductGUID,
                Characteristics = product.Characteristics,
                InUse = product.InUse,
                Category = product.Category,
                Name = product.Name,
                ImageLink = product.ImageLink,
                BasePrice = product.BasePrice,
            };
        }
    }
}
