using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class Searches : DALObject, ISearches
    {
        public Searches(DatabaseContext context) : base(context)
        {
        }

        public Search Add(Search search)
        {
            EntityEntry<Search>? addedProduct = _context.Searches.Add(search);
            _context.SaveChanges();
            var newSearch = GetByUid(addedProduct.Entity.SearchGUID);
            return newSearch;
        }

        public void Delete(Guid uid)
        {
            Search search = _context.Searches.FirstOrDefault(s => s.SearchGUID == uid);
            if (search == null)
                return;

            _context.Searches.Remove(search);
            _context.SaveChanges();
        }

        public List<Search> GetAll()
        {
            return _context.Searches
                .Include(s => s.User)
                .Include(s => s.Product)
                .ToList();
        }

        public Search? GetByUid(Guid uid)
        {
            return _context.Searches
                .Include(s => s.User)
                .Include(s => s.Product)
                .FirstOrDefault(s => s.SearchGUID == uid);
        }

        public List<Search> GetSearchesByUser(Guid userUid)
        {
            return _context.Searches
                .Include(s => s.User)
                .Include(s => s.Product)
                .Where(s => s.UserGUID == userUid)
                .ToList();
        }

        public void Update(Search search)
        {
            Search oldSearch = _context.Searches.FirstOrDefault(s => s.SearchGUID == s.UserGUID);
            if (oldSearch == null) return;
            oldSearch.UserGUID = search.UserGUID;
            oldSearch.ProductGUID = search.ProductGUID;

            _context.Searches.Update(oldSearch);
            _context.SaveChanges();
        }
    }
}
