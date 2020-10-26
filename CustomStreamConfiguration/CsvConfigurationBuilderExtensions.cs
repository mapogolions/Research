using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders.Physical;
using System.IO;

namespace CustomStreamConfiguration
{
    public static class CsvConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddCsv(this IConfigurationBuilder configurationBuilder,
            string fileName)
        {
            var file = new PhysicalFileInfo(new FileInfo(fileName));
            configurationBuilder.Add(new CsvConfigurationSource { Stream = file.CreateReadStream() });
            return configurationBuilder;
        }
    }
}
