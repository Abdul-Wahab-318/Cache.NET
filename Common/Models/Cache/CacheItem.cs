using System;

namespace Common.Models.Cache
{
    public class CacheItem
    {
        public object Value;
        public DateTime TimeExpiry;
        public DateTime TimeCreated;
        public DateTime TimeModified;
        public uint hits;

        public CacheItem(object value)
        {
            Value = value;
            TimeCreated = DateTime.UtcNow;
            TimeModified = DateTime.UtcNow;
            hits = 0;
        }

        public CacheItem(object value, DateTime TimeExpiry)
        {
            Value = value;
            TimeCreated = DateTime.UtcNow;
            TimeModified = DateTime.UtcNow;
            this.TimeExpiry = TimeExpiry;
            hits = 0;
        }

        public void IncrementHits()
        {
            hits++;
        }

        public bool IsExpired()
        {
            DateTime now = DateTime.UtcNow;

            return TimeExpiry < now;

        }
    }
}