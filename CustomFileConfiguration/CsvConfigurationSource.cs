using Microsoft.Extensions.Configuration;

namespace CustomFileConfiguration
{
    public class CsvConfigurationSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new CsvConfigurationProvider(this);
        }
    }
}
