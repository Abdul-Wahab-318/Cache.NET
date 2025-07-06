using Common.Enums;
using Common.Models.Cache;

namespace Common.Models.Tcp
{
    public class TcpRequest
    {
        public CacheItemDTO CacheItem { get; set; }
        public string Key;
        public RequestMethod Method { get; set; }

        public TcpRequest()
        {
            this.Method = RequestMethod.ERROR;
            this.Key = string.Empty;
            this.CacheItem = new CacheItemDTO();
        }

        public TcpRequest(RequestMethod Method)
        {
            this.Method = Method;
            this.Key = string.Empty;
            this.CacheItem = new CacheItemDTO();
        }

        public TcpRequest(RequestMethod Method, string Key, CacheItemDTO cacheItem)
        {
            this.Method = Method;
            this.Key = Key;
            this.CacheItem = cacheItem;
        }
        public TcpRequest(RequestMethod Method, string Key)
        {
            this.Method = Method;
            this.Key = Key;
            this.CacheItem = null;
        }

    }
}