using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface ISearches
    {
        List<Search> GetAll();
        Search? GetByUid(Guid uid);
        Search Add(Search assignment);
        void Update(Search assignment);
        void Delete(Guid uid);
        List<Search> GetUsersByUser(Guid userUid);
    }
}