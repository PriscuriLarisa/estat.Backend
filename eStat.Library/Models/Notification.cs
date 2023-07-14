namespace eStat.Library.Models
{
    public class Notification
    {
        public Guid NotificationGUID { get; set; }
        public Guid UserGUID { get; set; }
        public DateTime Date { get; set; }
        public bool Read { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Hyperlink { get; set; }
        public string HyperlinkText { get; set; }
    }
}