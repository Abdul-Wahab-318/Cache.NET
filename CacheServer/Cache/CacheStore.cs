using Common.Models.Cache;
using System.Collections.Generic;

namespace CacheServer.Cache
{
    internal class CacheStore : ICacheStore
    {
        private Dictionary<string, CacheItem> _store;
        public CacheStore()
        {
            _store = new Dictionary<string, CacheItem>();
        }
        public void Initialize()
        {

        }
        public void Add(string key, CacheItem value)
        {
            if(key == null) throw new ArgumentNullException("cache key cannot be null");

            lock(_store)
            {
                _store.Add(key, value);
            }
        }
        public CacheItem Get(string key)
        {
            if (key == null) throw new ArgumentNullException("cache key cannot be null");

            if(_store.ContainsKey(key))
                return _store[key];

            return null;
        }

        public CacheItem Update(string key, CacheItem value)
        {
            throw new NotImplementedException();
        }
        public void Remove(string key)
        {
            throw new NotImplementedException ();
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}