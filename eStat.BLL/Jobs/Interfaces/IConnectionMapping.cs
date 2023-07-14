namespace eStat.BLL.Jobs.Interfaces
{
    public interface IConnectionMapping
    {
        public void Add(string userUid, string connectionId);
        public IEnumerable<string> GetConnections(string userUid);
        public void Remove(string userUids, string connectionId);
    }
}
