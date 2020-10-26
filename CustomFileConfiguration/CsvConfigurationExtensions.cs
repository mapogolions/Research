using System;
using Microsoft.Extensions.Configuration;

namespace CustomFileConfiguration
{
    public static class CsvConfigurationExtentions
    { 
        public static IConfigurationBuilder AddCsvFile(this IConfigurationBuilder builder, 
            Action<CsvConfigurationSource> configureSource) => builder.Add(configureSource);
    }
}
