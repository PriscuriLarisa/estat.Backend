using eStat.Common.Enums;
using eStat.DAL.Entities;

namespace eStat.Library.Helpers
{
    public static class SortingHelper
    {
        public static List<Library.Models.Product> GetSortedProducts(List<Library.Models.Product> products, SortingCriteria sortingCriteria)
        {
            switch(sortingCriteria)
            {
                case SortingCriteria.AlphabeticAscending: 
                    products.Sort((a, b) => a.Name.CompareTo(b.Name));
                    return products;

                case SortingCriteria.AlphabeticDescending:
                    products.Sort((a, b) => b.Name.CompareTo(a.Name));
                    return products;

                case SortingCriteria.PriceAscending:
                    products.Sort((a, b) => a.BasePrice.CompareTo(b.BasePrice));
                    return products;

                case SortingCriteria.PriceDescending:
                    products.Sort((a, b) => b.BasePrice.CompareTo(a.BasePrice));
                    return products;
                default:
                    return products;
            }
        }

        public static List<DAL.Entities.Product> GetSortedProducts(List<DAL.Entities.Product> products, SortingCriteria sortingCriteria)
        {
            switch (sortingCriteria)
            {
                case SortingCriteria.AlphabeticAscending:
                    products.Sort((a, b) => a.Name.CompareTo(b.Name));
                    return products;

                case SortingCriteria.AlphabeticDescending:
                    products.Sort((a, b) => b.Name.CompareTo(a.Name));
                    return products;

                case SortingCriteria.PriceAscending:
                    products.Sort((a, b) => a.BasePrice.CompareTo(b.BasePrice));
                    return products;

                case SortingCriteria.PriceDescending:
                    products.Sort((a, b) => b.BasePrice.CompareTo(a.BasePrice));
                    return products;
                default:
                    return products;
            }
        }
    }
}
