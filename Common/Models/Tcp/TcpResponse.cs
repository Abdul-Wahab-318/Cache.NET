using Common.Models.Cache;

namespace Common.Models.Tcp
{
    public class TcpResponse
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public CacheItemDTO? CacheItem { get; set; }

        public TcpResponse()
        {
            Message = string.Empty;
            IsSuccess = false;
            CacheItem = new CacheItemDTO();
        }

        public TcpResponse(CacheItemDTO cacheItem, string message, bool isSuccess)
        {
            this.CacheItem = cacheItem;
            this.Message = message;
            this.IsSuccess = isSuccess;
        }

        public TcpResponse(string Message, bool isSuccess)
        {
            this.Message = Message;
            this.IsSuccess = isSuccess;
            this.CacheItem = null;
        }
    }
}