using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;
using System.Linq;

namespace eStat.BLL.Implementations
{
    public class SearchesBL : BusinessObject, ISearches
    {
        public SearchesBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public Search Add(Search search)
        {
            search.SearchGUID = Guid.Empty;
            search.Date = DateTime.Now;
            return SearchConverter.ToDTO(_dalContext.Searches.Add(SearchConverter.ToEntity(search)));
        }

        public Search AddWithDate(Search assignment, DateTime date)
        {
            assignment.SearchGUID = Guid.Empty;
            assignment.Date = date;
            DAL.Entities.Search newSearch = new DAL.Entities.Search
            {
                SearchGUID = assignment.SearchGUID,
                Date = assignment.Date,
                UserGUID = assignment.UserGUID,
                User = null,
                ProductGUID = assignment.ProductGUID,
                Product = null
            };
            return SearchConverter.ToDTO(_dalContext.Searches.Add(newSearch));
        }

        public List<Search> GetAll()
        {
            return _dalContext.Searches.GetAll().Select(s => SearchConverter.ToDTO(s)).ToList();
        }

        public List<Search> GetByProduct(Guid productGuid)
        {
            return _dalContext.Searches.GetAll().Where(s => s.ProductGUID == productGuid).Select(s => SearchConverter.ToDTO(s)).ToList();
        }

        public List<Search> GetByProductFromLastMonth(Guid productGuid)
        {
            return _dalContext.Searches.GetAll().Where(s => (DateTime.Now - s.Date).TotalDays <= 30 &&
                                                            s.ProductGUID == productGuid)
                .Select(s => SearchConverter.ToDTO(s))
                .ToList();
        }

        public Search? GetByUid(Guid uid)
        {
            var search = _dalContext.Searches.GetByUid(uid);
            if (search == null)
            {
                return null;
            }
            return SearchConverter.ToDTO(search);
        }

        public List<Search> GetByUser(Guid userUid)
        {
            return _dalContext.Searches.GetAll().Where(s => s.UserGUID == userUid).Select(s => SearchConverter.ToDTO(s)).ToList();
        }

        public List<Search> GetByUserFromLastMonth(Guid userUid)
        {
            return _dalContext.Searches.GetAll().Where(s => (DateTime.Now - s.Date).TotalDays <= 30 &&
                                                            s.UserGUID == userUid)
                .Select(s => SearchConverter.ToDTO(s))
                .ToList();
        }

        public Dictionary<Guid, int> GetNbOfSearchesByProductFromLastMonth(List<Guid> productUids)
        {
            List<Search> searches = _dalContext.Searches.GetAll().Where(s => (DateTime.Now - s.Date).TotalDays <= 30).Select(s => SearchConverter.ToDTO(s)).ToList();
            Dictionary<Guid, int> dictionary = new();
            foreach (Search search in searches)
            {
                if (!productUids.Contains(search.ProductGUID))
                    continue;

                if (dictionary.ContainsKey(search.ProductGUID))
                {
                    dictionary[search.ProductGUID] += 1;
                    continue;
                }
                dictionary.Add(search.ProductGUID, 1);
            }
            return dictionary;
        }
    }
}