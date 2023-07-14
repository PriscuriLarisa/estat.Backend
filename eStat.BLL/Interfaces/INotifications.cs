using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface INotifications
    {
        List<Notification> GetAll();
        Notification? GetByUid(Guid uid);
        Notification Add(Notification order);
        void Update(Notification order);
        void Delete(Guid uid);
        List<Notification> GetByUser(Guid userUid);
        void ReadAllUserNotifications(Guid userUid);
    }
}
