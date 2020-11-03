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
                    new KeyValuePair<string, string>("Env", "Development"),
                    new KeyValuePair<string, string>("Development:Host", "127.0.0.1"),
                    new KeyValuePair<string, string>("Development:Port", "8000"),
                    new KeyValuePair<string, string>("Flag", "true")
                })
                .Build();
            var devConfigSection = configRoot.GetSection("development");
            var config = new Config
            {
                Env = configRoot.GetValue("env", "default env"),
                Host = devConfigSection.GetValue("host", "localhost"),
                Port = devConfigSection.GetValue("port", 25000),
                Flag = configRoot.GetValue("flag", false)
            };
            Console.WriteLine(config);
        }
    }
}
