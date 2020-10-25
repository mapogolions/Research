using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

namespace InMemoryConfiguration
{
    internal static class Program
    {
        internal static void Main()
        {
            var configRoot = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource // will be checked last
                {
                    InitialData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("env", "Development"),
                        new KeyValuePair<string, string>("host", "localhost"),
                        new KeyValuePair<string, string>("port", "9000"),
                    }
                })
                .AddInMemoryCollection(new List<KeyValuePair<string, string>> // will be checked first
                {
                    new KeyValuePair<string, string>("env", "Production")
                })
                .Build();

            Console.WriteLine($"{configRoot["env"]}|{configRoot["host"]}|{configRoot["port"]}");
        }
    }
}
