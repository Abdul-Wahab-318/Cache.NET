using CacheServer.Cache;
using Common.Mappers;

namespace CacheServer.Commands
{
    internal abstract class CommandBase
    {
        protected ICacheStore _cache;
        protected CacheItemMapper _mapper;

        public CommandBase(ICacheStore _cache)
        {
            this._cache = _cache;
            this._mapper = new CacheItemMapper();
        }

        public abstract CommandResponse Execute();

    }
}
