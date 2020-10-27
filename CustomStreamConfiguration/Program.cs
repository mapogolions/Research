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

            var devConfigSection = configRoot.GetSection("development");
            Console.WriteLine($"{configRoot["env"]}|{devConfigSection["host"]}|{devConfigSection["port"]}");
        }
    }
}
