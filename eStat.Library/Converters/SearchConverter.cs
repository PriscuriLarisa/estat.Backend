using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStat.Library.Converters
{
    public static class SearchConverter
    {
        public static Library.Models.Search ToDTO(DAL.Entities.Search search)
        {
            return new Library.Models.Search
            {
                SearchGUID = search.SearchGUID,
                ProductGUID= search.ProductGUID,
                Product = ProductConverter.ToDTO(search.Product),
                UserGUID = search.UserGUID,
                User = UserConverter.ToDTOInfo(search.User),
                Date = search.Date
            };
        }

        public static DAL.Entities.Search ToEntity(Library.Models.Search search)
        {
            return new DAL.Entities.Search
            {
                SearchGUID = search.SearchGUID,
                ProductGUID = search.ProductGUID,
                //Product = ProductConverter.ToEntity(order.Product),
                UserGUID = search.UserGUID,
                Date = search.Date,
                //User = UserConverter.ToEntity(order.User),
            };
        }
    }
}
