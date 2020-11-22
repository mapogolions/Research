using System;

namespace ConfigureOptionsMonitor
{
    public class CacheEntryOptions
    {
        public DateTime? AbsoluteExpiration { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public override string ToString()
        {
            return $"CacheEntry({AbsoluteExpiration}, {SlidingExpiration})";
        }
    }
}
