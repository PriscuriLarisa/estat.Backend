using eStat.DAL.Entities;

namespace eStat.DAL.Interfaces
{
    public interface ISearches
    {
        List<Search> GetAll();
        Search? GetByUid(Guid uid);
        Search Add(Search search);
        void Update(Search search);
        void Delete(Guid uid);
        List<Search> GetSearchesByUser(Guid userUid);
    }
}
