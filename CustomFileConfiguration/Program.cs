using Microsoft.Extensions.Configuration;
using System;

namespace CustomFileConfiguration
{
    internal class Program
    {
        internal static void Main()
        {
            var configRoot = new ConfigurationBuilder()
                .AddCsvFile(s =>
                {
                    s.Path = "./config/settings.csv";
                    // s.ResolveFileProvider(); uncomment this if you use is rooted path
                })
                .Build();

            var devConfigSection = configRoot.GetSection("Development");
            Console.WriteLine($"{configRoot["env"]}|{devConfigSection["host"]}|{devConfigSection["port"]}");
        }
    }
}
