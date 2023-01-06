namespace Services.Models.ChatHubClass
{
    public class ChatConnectionMapping
    {
        private readonly Dictionary<string, HashSet<string>> _connections =
            new Dictionary<string, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(string key, string connectionId)
        {

            HashSet<string> connections;
            if (!_connections.TryGetValue(key, out connections))
            {
                connections = new HashSet<string>();
                _connections.Add(key, connections);
            }
            connections.Add(connectionId);
        }

        public IEnumerable<string> GetConnections(string key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(string key, string connectionId)
        {

            HashSet<string> connections;
            if (!_connections.TryGetValue(key, out connections))
            {
                return;
            }

            connections.Remove(connectionId);

            if (connections.Count == 0)
            {
                _connections.Remove(key);
            }
        }
        public List<string> GetConnectedUsersIds()
        {
            return _connections.Keys.Any() ? _connections.Keys.ToList() : new List<string>();
        }
    }
}
