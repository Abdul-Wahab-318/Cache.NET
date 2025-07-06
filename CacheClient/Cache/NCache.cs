using CacheClient.Connection;
using Common.Models.Tcp;
using Common.Models.Cache;
using Common.Enums;

namespace CacheClient.Cache
{
    public class NCache : ICache
    {
        public string CacheName;
        private string _ip;
        private int _port;
        private IServerConnection? _connection;

        public NCache(string ip, int port, string cacheName)
        {
            _ip = ip;
            _port = port;
            CacheName = cacheName;
        }

        public void Initialize()
        {
            _connection = new ServerConnection(this._ip, this._port);
            _connection.Connect();
        }

        // Add method throws duplicate key exception if already exists.
        public void Add(string key, object value)
        {
            if (value == null)
                throw new ArgumentNullException("Cache Item cannot be null :(");

            CacheItemDTO item = new CacheItemDTO(value);
            TcpRequest request = new TcpRequest(Method: RequestMethod.ADD, key, item);

            TcpResponse response = _connection.Send(request);
            Console.WriteLine("ADD Response : " + response.Message + "\nSuccess : " + response.IsSuccess);
        }

        // Returns the cached object against the given key. Returns null if it does not exist.
        public object Get(string key)
        {
            if (key == null)
                throw new ArgumentNullException("Cache Key cannot be null");

            TcpRequest request = new TcpRequest(Method: RequestMethod.GET, key);

            TcpResponse response = _connection.Send(request);
            Console.WriteLine("GET Response : " + response.Message + "\nSuccess : " + response.IsSuccess);
            return response.CacheItem;
        }

        // Updates the complete object in the cache for the given key. Throws exception if key does not exist.
        public void Update(string key, object value)
        {
            if (key == null)
                throw new ArgumentNullException("Cache Key cannot be null");

            CacheItemDTO item = new CacheItemDTO(value);
            TcpRequest request = new TcpRequest(Method: RequestMethod.UPDATE, key, item);

            TcpResponse response = _connection.Send(request);
            Console.WriteLine("UPDATE Response : " + response.Message + "\nSuccess : " + response.IsSuccess);
        }

        // Removes the object from cache against the given key. Does nothing if it does not exist.
        public void Remove(string key)
        {
            if (key == null)
                throw new ArgumentNullException("Cache Key cannot be null");

            TcpRequest request = new TcpRequest(Method: RequestMethod.UPDATE, key);

            TcpResponse response = _connection.Send(request);
            Console.WriteLine("UPDATE Response : " + response.Message + "\nSuccess : " + response.IsSuccess);
        }

        // Clears the cache.	
        public void Clear()
        {
            TcpRequest request = new TcpRequest(Method: RequestMethod.CLEAR);

            TcpResponse response = _connection.Send(request);
            Console.WriteLine("UPDATE Response : " + response.Message + "\nSuccess : " + response.IsSuccess);
        }

        // Disposes the ICache instance and all underlying connections.
        public void Dispose()
        {
            _connection.Disconnect();
        }
    }
}