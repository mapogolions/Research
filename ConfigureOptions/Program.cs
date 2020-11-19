using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Linq;

namespace ConfigureOptions
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurations = new List<IConfigureOptions<CacheEntryOptions>>
            {
                new ConfigureNamedOptions<CacheEntryOptions>("OneDayExpiration", it =>
                {
                    it.AbsoluteExpiration = DateTime.Now + TimeSpan.FromDays(1);
                    it.SlidingExpiration = TimeSpan.FromMinutes(2);
                }),
                new ConfigureNamedOptions<CacheEntryOptions>("NeverRemove", it =>
                {
                    it.AbsoluteExpiration = DateTime.MaxValue;
                    it.SlidingExpiration = TimeSpan.MaxValue;
                }),
                new ConfigureOptions<CacheEntryOptions>(it =>
                {
                    it.AbsoluteExpiration = DateTime.Now + TimeSpan.FromDays(2);
                    it.SlidingExpiration = TimeSpan.FromHours(2);
                })
            };

            var validations = new List<IValidateOptions<CacheEntryOptions>>
            {
                new ValidateOptions<CacheEntryOptions>(null /* means catch all */,
                    it => it.AbsoluteExpiration != null && it.SlidingExpiration != null, "Field should be initialized"),
                new ValidateOptions<CacheEntryOptions, DateTime>(null, DateTime.Now,
                    (it, now) => it.AbsoluteExpiration > now, "Entry shoudn't be expired")
            };

            var cacheEntryOptionsFactory = new OptionsFactory<CacheEntryOptions>(configurations,
                Enumerable.Empty<IPostConfigureOptions<CacheEntryOptions>>(), validations);
            var cacheEntryOptionsManager = new OptionsManager<CacheEntryOptions>(cacheEntryOptionsFactory);
            var neverRemoveCacheEntryOptions = cacheEntryOptionsManager.Get("NeverRemove");
            Console.WriteLine(neverRemoveCacheEntryOptions);
            var oneDayExpirationCacheEntryOptions = cacheEntryOptionsManager.Get("OneDayExpiration");
            Console.WriteLine(oneDayExpirationCacheEntryOptions);
            var defaultCacheEntryOptions = cacheEntryOptionsManager.Get(string.Empty);
            Console.WriteLine(defaultCacheEntryOptions);
        }
    }
}
