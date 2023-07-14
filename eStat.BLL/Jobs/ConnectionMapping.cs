using eStat.BLL.Jobs.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eStat.BLL.Jobs
{
    public class ConnectionMapping : IConnectionMapping
    {
        private readonly Dictionary<string, HashSet<string>> _connections = new Dictionary<string, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(string userUid, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(userUid, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(userUid, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(string userUid)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(userUid, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(string userUids, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(userUids, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(userUids);
                    }
                }
            }
        }
    }
}
