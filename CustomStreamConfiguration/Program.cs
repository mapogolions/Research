using System;
using Microsoft.Extensions.Configuration;

namespace CustomStreamConfiguration
{
    internal class Program
    {
        internal static void Main()
        {
            var configRoot = new ConfigurationBuilder()
                .AddCsv("config/settings.csv")
                .Build();

            Console.WriteLine($"{configRoot["env"]}|{configRoot["host"]}|{configRoot["port"]}");
        }
    }
}
