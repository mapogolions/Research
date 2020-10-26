using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace UserSecretConfiguration
{
    internal static class Program
    {
        internal static void Main()
        {
            var configRoot = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();

            Console.WriteLine($"{configRoot["env"]}|{configRoot["host"]}|{configRoot["port"]}");
        }
    }
}
