using System;

namespace Common.Models.Cache
{
    public class CacheItemDTO
    {
        public CacheItemDTO()
        {
            Value = string.Empty;
            TimeExpiry = DateTime.MinValue;
        }

        public CacheItemDTO(object value, DateTime timeExpiry)
        {
            Value = value;
            TimeExpiry = timeExpiry;
        }

        public CacheItemDTO(object value)
        {
            Value = value;
        }

        public object Value { get; set; }
        public DateTime TimeExpiry { get; set; }
    }
}