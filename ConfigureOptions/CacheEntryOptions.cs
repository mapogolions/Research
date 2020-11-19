using System;

namespace ConfigureOptions
{
    public class CacheEntryOptions
    {
        public DateTime? AbsoluteExpiration { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public override string ToString()
        {
            return $"CacheEntryOptions({AbsoluteExpiration}, {SlidingExpiration})";
        }
    }
}
