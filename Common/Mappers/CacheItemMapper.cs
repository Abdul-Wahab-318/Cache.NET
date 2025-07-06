using System;
using Common.Models.Cache;

namespace Common.Mappers
{
    public class CacheItemMapper
    {
        public CacheItem ToCacheItem(CacheItemDTO dto)
        {
            CacheItem item = new CacheItem(dto.Value, dto.TimeExpiry);
            return item;
        }

        public CacheItemDTO ToDto(CacheItem item)
        {
            CacheItemDTO dto = new CacheItemDTO(item.Value, item.TimeExpiry);
            return dto;
        }
    }
}