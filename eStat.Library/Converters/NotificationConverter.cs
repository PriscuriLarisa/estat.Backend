namespace eStat.Library.Converters
{
    public class NotificationConverter
    {
        public static Library.Models.Notification ToDTO(DAL.Entities.Notification notif)
        {
            return new Library.Models.Notification
            {
                NotificationGUID = notif.NotificationGUID,
                UserGUID = notif.UserGUID,
                Date = notif.Date,
                Title= notif.Title,
                Text= notif.Text,
                Hyperlink = notif.Hyperlink,
                HyperlinkText = notif.HyperlinkText,
                Read = notif.Read,
            };
        }

        public static DAL.Entities.Notification ToEntity(Library.Models.Notification notif)
        {
            return new DAL.Entities.Notification
            {
                NotificationGUID = notif.NotificationGUID,
                UserGUID = notif.UserGUID,
                Date = notif.Date,
                Title= notif.Title,
                Text= notif.Text,
                Hyperlink = notif.Hyperlink,
                HyperlinkText = notif.HyperlinkText,
                Read = notif.Read,
            };
        }
    }
}
