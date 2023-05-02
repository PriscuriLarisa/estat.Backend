using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.Common.Enums;
using eStat.DAL.Core.Context.Interfaces;
using eStat.DAL.Implementations;
using eStat.Library.Converters;
using eStat.Library.Helpers;
using eStat.Library.Models;

namespace eStat.BLL.Implementations
{
    public class ProductsBL : BusinessObject, IProducts
    {
        public ProductsBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public Product Add(Product product)
        {
            return ProductConverter.ToDTO(_dalContext.Products.Add(ProductConverter.ToEntity(product)));
        }

        public void Delete(Guid uid)
        {
            _dalContext.Products.Delete(uid);
        }

        public List<Product> GetAll()
        {
            return _dalContext.Products.GetAll().Select(p => ProductConverter.ToDTO(p)).ToList();
        }

        public Product? GetByUid(Guid uid)
        {
            DAL.Entities.Product result = _dalContext.Products.GetByUid(uid);
            if (result == null) return null;

            return ProductConverter.ToDTO(result);
        }

        public int GetNumberOfProductsBySearch(List<string> keyWords)
        {
            List<DAL.Entities.Product> allProducts = _dalContext.Products.GetAll();
            if (keyWords != null && keyWords.Count > 0)
            {
                allProducts = allProducts.Where(p => keyWords.Any(kw => p.Characteristics.Contains(kw) || p.Name.Contains(kw))).ToList();
            }
            return allProducts.Count;
        }

        public List<string> GetProductCategories()
        {
            return _dalContext.Products.GetProductsCategories();
        }

        public List<Product> GetProductsByLastPage(int itemsPerPage)
        {
            List<DAL.Entities.Product> products = _dalContext.Products.GetAll();
            int nbofPages = decimal.ToInt32(decimal.Floor(products.Count / itemsPerPage));
            return products.Skip(nbofPages * itemsPerPage).Select(p => ProductConverter.ToDTO(p)).ToList();
        }

        public List<Product> GetProductsByPage(int pageNumber, int productsPerPage, string category, SortingCriteria sortingCriteria = SortingCriteria.AlphabeticAscending)
        {
            List<DAL.Entities.Product> allProducts = _dalContext.Products.GetAll();
            if(category != null)
            {
                allProducts = allProducts.Where(p => p.Category == category).ToList();
            }
            List<DAL.Entities.Product> products = SortingHelper.GetSortedProducts(allProducts, sortingCriteria);

            return products.Skip(pageNumber * productsPerPage).Take(productsPerPage).Select(p => ProductConverter.ToDTO(p)).ToList();
        }

        public List<Product> GetSearchedProductsByPage(int pageNumber, int productsPerPage, List<string> keyWords, SortingCriteria sortingCriteria)
        {
            List<DAL.Entities.Product> allProducts = _dalContext.Products.GetAll();
            if(keyWords != null && keyWords.Count > 0)
            {
                allProducts = allProducts.Where(p => keyWords.Any(kw => p.Characteristics.Contains(kw) || p.Name.Contains(kw))).ToList();
            }
            List<DAL.Entities.Product> products = SortingHelper.GetSortedProducts(allProducts, sortingCriteria);
            return products.Skip(pageNumber * productsPerPage).Take(productsPerPage).Select(p => ProductConverter.ToDTO(p)).ToList();
        }

        public void Update(Product product)
        {
            _dalContext.Products.Update(ProductConverter.ToEntity(product));
        }
    }
}
