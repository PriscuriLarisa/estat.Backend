using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStat.Library.Converters
{
    public static class ProductRequestConverter
    {
        public static Library.Models.ProductRequest ToDTO(DAL.Entities.ProductRequest purchase)
        {
            return new Library.Models.ProductRequest
            {
                ProductRequestGUID = purchase.ProductRequestGUID,
                Price= purchase.Price,
                UserGUID= purchase.UserGUID,
                ProductGUID = purchase.ProductGUID,
                Product = ProductConverter.ToDTO(purchase.Product),
                User = UserConverter.ToDTO(purchase.User)
            };
        }

        public static DAL.Entities.ProductRequest ToEntity(Library.Models.ProductRequest purchase)
        {
            return new DAL.Entities.ProductRequest
            {
                ProductRequestGUID = purchase.ProductRequestGUID,
                Price = purchase.Price,
                UserGUID = purchase.UserGUID,
                ProductGUID = purchase.ProductGUID,
            };
        }
    }
}
