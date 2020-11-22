using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace ConfigureOptionsMonitor
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            var configurations = new CacheEntryOptionsConfigurations()
                .AddConfiguration("OneDayExpiration", it =>
                {
                    it.AbsoluteExpiration = DateTime.Now + TimeSpan.FromDays(1);
                    it.SlidingExpiration = TimeSpan.FromMinutes(2);
                })
                .AddConfiguration("NeverRemove", it => {
                    it.AbsoluteExpiration = DateTime.MaxValue;
                    it.SlidingExpiration = TimeSpan.MaxValue;
                })
                .AddConfiguration(Options.DefaultName, it => {
                    it.AbsoluteExpiration = DateTime.Now + TimeSpan.FromDays(2);
                    it.SlidingExpiration = TimeSpan.FromHours(2);
                });

            var validations = new List<IValidateOptions<CacheEntryOptions>>
            {
                new ValidateOptions<CacheEntryOptions>(null /* means catch all */,
                    it => it.AbsoluteExpiration != null && it.SlidingExpiration != null, "Field should be initialized"),
                new ValidateOptions<CacheEntryOptions, DateTime>(null, DateTime.Now,
                    (it, now) => it.AbsoluteExpiration > now, "Entry shoudn't be expired")
            };

            var cacheEntryOptionsFactory = new OptionsFactory<CacheEntryOptions>(configurations,
                Enumerable.Empty<IPostConfigureOptions<CacheEntryOptions>>(), validations);
            var cacheEntryOptionsMonitor = new OptionsMonitor<CacheEntryOptions>(cacheEntryOptionsFactory,
                configurations.Cancellations, new OptionsCache<CacheEntryOptions>());

            Console.WriteLine(cacheEntryOptionsMonitor.Get("NeverRemove"));
            Console.WriteLine(cacheEntryOptionsMonitor.Get("OneDayExpiration"));
            Console.WriteLine(cacheEntryOptionsMonitor.Get(Options.DefaultName));

            // change default configuration
            configurations.AddConfiguration(Options.DefaultName, it => {
                it.AbsoluteExpiration = DateTime.Now + TimeSpan.FromDays(2);
                it.SlidingExpiration = TimeSpan.FromHours(3);
            });
            Console.WriteLine(cacheEntryOptionsMonitor.Get(Options.DefaultName));

            // change default configuration
            configurations.AddConfiguration(Options.DefaultName, it => {
                it.AbsoluteExpiration = DateTime.Now + TimeSpan.FromDays(2);
                it.SlidingExpiration = TimeSpan.FromHours(10);
            });

            Console.WriteLine(cacheEntryOptionsMonitor.Get(Options.DefaultName));
        }
    }
}
