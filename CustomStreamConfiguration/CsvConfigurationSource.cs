using Microsoft.Extensions.Configuration;

namespace CustomStreamConfiguration
{
    public class CsvConfigurationSource : StreamConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CsvConfigurationProvider(this);
        }
    }
}
