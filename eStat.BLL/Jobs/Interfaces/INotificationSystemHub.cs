namespace eStat.BLL.Jobs.Interfaces
{
    public interface INotificationSystemHub
    {
        public Task NotifyUser(string userId, string message);
    }
}