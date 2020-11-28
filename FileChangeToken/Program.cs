using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders.Physical;

namespace FileChangeToken
{
    internal static class Program
    {
        internal static async Task Main()
        {
            await Case2();
        }

        internal static async Task Case1()
        {
            var settingsFileInfo = new FileInfo("./settings.json");
            var pollingFileChangeToken = new PollingFileChangeToken(settingsFileInfo);
            settingsFileInfo.LastWriteTimeUtc = DateTime.UtcNow;
            Console.WriteLine(pollingFileChangeToken.HasChanged);
            await Task.CompletedTask;
        }

        internal static async Task Case2()
        {
            var settingsFileInfo = new FileInfo("./settings.json");
            var pollingFileChangeToken = new PollingFileChangeToken(settingsFileInfo);
            Console.WriteLine(pollingFileChangeToken.HasChanged);
            settingsFileInfo.LastWriteTimeUtc = DateTime.UtcNow;
            await Task.Delay(TimeSpan.FromSeconds(3)); // default polling interval 4 seconds
            Console.WriteLine(pollingFileChangeToken.HasChanged);
        }
    }
}
