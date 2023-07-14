using eStat.DAL.Entities;

namespace eStat.DAL.Interfaces
{
    public interface INotifications
    {
        List<Notification> GetAll();
        Notification? GetByUid(Guid uid);
        Notification Add(Notification assignment);
        void Update(Notification assignment);
        void Delete(Guid uid);
    }
}