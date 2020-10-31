using System;
using Microsoft.Extensions.Configuration;

namespace EnvVarsConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            var configRoot = new ConfigurationBuilder()
                .AddEnvironmentVariables("Temp")
                .Build();
            var devConfig = configRoot.GetSection("development");
            Console.WriteLine($"{configRoot["env"]}|{devConfig["host"]}|{devConfig["port"]}");
        }
    }
}
