using System.Threading;
using System;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ConfigureOptionsMonitor
{
    public class CacheEntryOptions
    {
        public DateTime? AbsoluteExpiration { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public override string ToString()
        {
            return $"CacheEntry({AbsoluteExpiration}, {SlidingExpiration})";
        }
    }

    public class CacheEntryOptionsChangeTokenSource : IOptionsChangeTokenSource<CacheEntryOptions>
    {
        public string Name { get; }
        private readonly IChangeToken _changeToken;

        public CacheEntryOptionsChangeTokenSource(string name, IChangeToken changeToken)
        {
            Name = name;
            _changeToken = changeToken;
        }

        public IChangeToken GetChangeToken() => _changeToken;
    }

    public class CacheEntryOptionsConfigurations
        : IEnumerable<IConfigureOptions<CacheEntryOptions>>
    {
        private readonly IDictionary<string, IConfigureOptions<CacheEntryOptions>> _configurations =
            new Dictionary<string, IConfigureOptions<CacheEntryOptions>>();
        private readonly IDictionary<string, CancellationTokenSource> _cancellationTokenSources;

        public CacheEntryOptionsConfigurations Add(string name, Action<CacheEntryOptions> action)
        {
            if (_configurations.ContainsKey(name))
            {
                if (!_cancellationTokenSources.TryGetValue(name, out var cts))
                    throw new InvalidOperationException("something goes wrong");
                cts.Cancel();
                _configurations.Remove(name);
            }
            _cancellationTokenSources.Add(name, new CancellationTokenSource());
            var options = new ConfigureNamedOptions<CacheEntryOptions>(name, action);
            _configurations.Add(name, options);
            return this;
        }

        public IEnumerable<IOptionsChangeTokenSource<CacheEntryOptions>> Cancellations =>
            _cancellationTokenSources.Select(kv => new CacheEntryOptionsChangeTokenSource(kv.Key, new CancellationChangeToken(kv.Value.Token)));

        public IEnumerator<IConfigureOptions<CacheEntryOptions>> GetEnumerator() => _configurations.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
