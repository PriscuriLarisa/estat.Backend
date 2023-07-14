using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class Notifications : DALObject, INotifications
    {
        public Notifications(DatabaseContext context) : base(context)
        {
        }

        public Notification Add(Notification notification)
        {
            EntityEntry<Notification>? addedNotification = _context.Notifications.Add(notification);
            _context.SaveChanges();
            return addedNotification.Entity;
        }

        public void Delete(Guid uid)
        {
            Notification notif = _context.Notifications.FirstOrDefault(n => n.NotificationGUID == uid);
            if (notif == null)
                return;

            _context.Notifications.Remove(notif);
            _context.SaveChanges();
        }

        public List<Notification> GetAll()
        {
            return _context.Notifications.ToList();
        }

        public Notification? GetByUid(Guid uid)
        {
            return _context.Notifications.FirstOrDefault(n => n.NotificationGUID == uid);
        }

        public void Update(Notification notif)
        {
            Notification oldNotification = _context.Notifications.FirstOrDefault(o => o.NotificationGUID == notif.NotificationGUID);
            if (oldNotification == null) return;
            oldNotification.Read = notif.Read;

            _context.Notifications.Update(oldNotification);
            _context.SaveChanges();
        }
    }
}