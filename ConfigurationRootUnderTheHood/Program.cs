using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System.Collections.Generic;

namespace ConfigurationRootUnderTheHood
{
    internal class Program
    {
        internal static void Main()
        {
            var configSource = new MemoryConfigurationSource
            {
                InitialData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("env", "Development"),
                    new KeyValuePair<string, string>("host", "localhost"),
                    new KeyValuePair<string, string>("port", "9000"),
                }
            };
            var configProvider = new MemoryConfigurationProvider(new MemoryConfigurationSource
            {
                InitialData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("env", "Production")
                }
            });
            var configProviders = new List<IConfigurationProvider> 
            {
                configSource.Build(null), // like configProvider above
                configProvider
            };
            var configRoot = new ConfigurationRoot(configProviders);
            Console.WriteLine($"{configRoot["env"]}|{configRoot["host"]}|{configRoot["port"]}");
        }
    }
}
