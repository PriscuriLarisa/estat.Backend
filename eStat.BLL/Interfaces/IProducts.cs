using eStat.Common.Enums;
using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IProducts
    {
        List<Product> GetAll();
        List<Product> GetProductsByPage(int pageNumber, int productsPerPage, string category, SortingCriteria sortingCriteria);
        List<Product> GetSearchedProductsByPage(int pageNumber, int productsPerPage, List<string> keyWords, SortingCriteria sortingCriteria);
        int GetNumberOfProductsBySearch(List<string> keyWords);
        List<Product> GetProductsByLastPage(int itemsPerPage);
        List<string> GetProductCategories();
        Product? GetByUid(Guid uid);
        Product Add(Product product);
        void Update(Product product);
        void Delete(Guid uid);
        Dictionary<int, decimal> GetAvgPriceLastSixMonths(Guid uid);
        Dictionary<int, decimal> GetLowestPriceLastSixMonths(Guid uid);
        Dictionary<int, decimal> GetHighestPriceLastSixMonths(Guid uid);
        int GetSellsForHighestPrice(Guid productUid);
        int GetSellsForLowestPrice(Guid productUid);
        int GetSellsForAveragePrice(Guid productUid);
        Dictionary<int, decimal> GetSellsLastSixMonths(Guid uid);
        decimal GetAveragePrice(Guid productUid);
    }
}