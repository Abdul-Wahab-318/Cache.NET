using CacheServer.Cache;
using Common.Enums;
using Common.Mappers;
using Common.Models.Cache;
using Common.Models.Tcp;

namespace CacheServer.Commands
{
    internal class CommandFactory
    {
        protected ICacheStore _cache;
        protected CacheItemMapper _mapper;
        public CommandFactory(ICacheStore _cache)
        {
            this._cache = _cache;
            this._mapper = new CacheItemMapper();
        }
        
        public CommandBase CreateCommand(TcpRequest request)
        {
            CacheItem cacheItem = _mapper.ToCacheItem(request.CacheItem);

            switch(request.Method)
            {
                case RequestMethod.GET:
                    return new GetCommand(_cache ,request.Key);

                case RequestMethod.ADD:
                    return new AddCommand(_cache, request.Key , cacheItem);

                default:
                    throw new ArgumentException("Incorrect Request Method");
            }
        }

    }
}
