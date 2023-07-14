using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;

namespace eStat.BLL.Implementations
{
    public class NotificationsBL : BusinessObject, INotifications
    {
        public NotificationsBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public Notification Add(Notification notification)
        {
            return NotificationConverter.ToDTO(_dalContext.Notifications.Add(NotificationConverter.ToEntity(notification)));
        }

        public void Delete(Guid uid)
        {
            _dalContext.Notifications.Delete(uid);
        }

        public List<Notification> GetAll()
        {
            return _dalContext.Notifications.GetAll().Select(n => NotificationConverter.ToDTO(n)).ToList();
        }

        public Notification? GetByUid(Guid uid)
        {
            return NotificationConverter.ToDTO(_dalContext.Notifications.GetByUid(uid));
        }

        public List<Notification> GetByUser(Guid userUid)
        {
            return _dalContext.Notifications.GetAll().Where(n => n.UserGUID == userUid).Select(n => NotificationConverter.ToDTO(n)).ToList();
        }

        public void ReadAllUserNotifications(Guid userUid)
        {
            List<DAL.Entities.Notification> notifications = _dalContext.Notifications.GetAll().Where(n => n.UserGUID == userUid).ToList();
            foreach(DAL.Entities.Notification existingNotification in notifications)
            {
                DAL.Entities.Notification notification = new DAL.Entities.Notification
                {
                    NotificationGUID = existingNotification.NotificationGUID,
                    Title = existingNotification.Title,
                    Text = existingNotification.Text,
                    Hyperlink = existingNotification.Hyperlink,
                    HyperlinkText = existingNotification.HyperlinkText,
                    Read = true,
                    Date = existingNotification.Date,
                    UserGUID = userUid
                };
                _dalContext.Notifications.Update(notification);
            }
        }

        public void Update(Notification order)
        {
            throw new NotImplementedException();
        }
    }
}