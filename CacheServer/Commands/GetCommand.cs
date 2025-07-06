using CacheServer.Cache;
using Common.Mappers;
using Common.Models.Cache;
using Common.Models.Tcp;

namespace CacheServer.Commands
{
    internal class GetCommand : CommandBase
    {
        private CacheItem _item;
        private string _key;

        public GetCommand(ICacheStore _cache, string _key) : base(_cache)
        {
            this._key = _key;
        }

        public override CommandResponse Execute()
        {
            try
            {
                CacheItem cacheItem = _cache.Get(_key);
                CacheItemDTO cacheItemDTO = _mapper.ToDto(cacheItem);
                return new CommandResponse(cacheItemDTO, "Cache item retrieved", true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Failed to add cache item");
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
