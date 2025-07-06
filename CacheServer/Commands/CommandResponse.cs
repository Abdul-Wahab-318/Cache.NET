using Common.Models.Cache;

namespace CacheServer.Commands
{
    internal class CommandResponse
    {
        public string Message;
        public Boolean IsSuccess;
        public CacheItemDTO Item;

        public CommandResponse(CacheItemDTO item , string message , Boolean IsSuccess) 
        {
            this.Item = item;
            this.Message = message;
            this.IsSuccess = IsSuccess; 
        }

        public CommandResponse(string message, Boolean IsSuccess)
        {
            this.Item = null;
            this.Message = message;
            this.IsSuccess = IsSuccess;
        }
    }
}
