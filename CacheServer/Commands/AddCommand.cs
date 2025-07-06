using CacheServer.Cache;
using Common.Mappers;
using Common.Models.Cache;
namespace CacheServer.Commands
{
    internal class AddCommand : CommandBase
    {
        private CacheItem _item;
        private string _key;

        public AddCommand(ICacheStore _cache ,string _key, CacheItem item) : base(_cache)
        {
            this._key = _key;
            this._item = item;
        }

        public override CommandResponse Execute()
        {
            try
            {
                _cache.Add(_key, _item);
                return new CommandResponse(null, "Cache item added", true);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: Failed to add cache item");
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
