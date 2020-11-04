using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace TypedConfiguration
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            var configRoot = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Development:Host", "127.0.0.1"),
                    new KeyValuePair<string, string>("Development:Port", "8000"),
                    new KeyValuePair<string, string>("Development:Flags:ReloadOnChange", "true"),
                    new KeyValuePair<string, string>("Development:Flags:ShutdownGracefully", "true")
                })
                .Build();
            var devConfigSection = configRoot.GetSection("development");
            var config = new Config
            {
                Host = devConfigSection.GetValue("host", "localhost"),
                Port = devConfigSection.GetValue("port", 25000),
            };
            Console.WriteLine(config);
            Console.WriteLine(devConfigSection.Get<Config>());
            var flags = devConfigSection.GetSection("Flags").Get<IDictionary<string, bool>>();
            foreach (var flag in flags)
            {
                Console.WriteLine($"{flag.Key} -> {flag.Value}");
            }
        }
    }
}
