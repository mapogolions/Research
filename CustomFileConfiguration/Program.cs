using Microsoft.Extensions.Configuration;
using System;
using System.IO;

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

            Console.WriteLine($"{configRoot["env"]}|{configRoot["host"]}|{configRoot["port"]}");
        }
    }
}
