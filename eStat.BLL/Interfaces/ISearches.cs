using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface ISearches
    {
        List<Search> GetAll();
        Search? GetByUid(Guid uid);
        Search Add(Search assignment);
        Search AddWithDate(Search assignment, DateTime date);
        List<Search> GetByUser(Guid userUid);
        List<Search> GetByUserFromLastMonth(Guid userUid);
        List<Search> GetByProduct(Guid userUid);
        List<Search> GetByProductFromLastMonth(Guid userUid);
        Dictionary<Guid, int> GetNbOfSearchesByProductFromLastMonth(List<Guid> productUids);
    }
}