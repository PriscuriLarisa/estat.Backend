using eStat.BLL.Interfaces;

namespace eStat.BLL.Jobs.Interfaces
{
    public interface INotificationGenerator
    {
        public Dictionary<string, string> GenerateNotificationForPriceChanges();
    }
}
