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
            var env = configRoot["env"];
            var envConfig = configRoot.GetSection(env);
            Console.WriteLine($"{env}|{envConfig["host"]}|{envConfig["port"]}");
        }
    }
}
