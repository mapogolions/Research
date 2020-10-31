using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CustomFileConfiguration
{
    public class CsvConfigurationProvider : FileConfigurationProvider
    {
        public CsvConfigurationProvider(CsvConfigurationSource source) : base(source) {  }

        public override void Load(Stream stream)
        {
            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var chunks = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (chunks.Length != 2)
                    throw new InvalidOperationException("Invalid csv format");
                Data.Add(chunks[0].Trim(), chunks[1].Trim());
            }
        }
    }
}
