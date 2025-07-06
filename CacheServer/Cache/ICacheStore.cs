using Common.Models.Cache;

namespace CacheServer.Cache
{
    internal interface ICacheStore
    {
        public void Add(string key, CacheItem value);
        public CacheItem Get(string key);
        public CacheItem Update(string key, CacheItem value);
        public void Remove(string key);
        public void Clear();
    }
}
