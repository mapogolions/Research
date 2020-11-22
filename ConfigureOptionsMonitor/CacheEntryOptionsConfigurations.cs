using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace ConfigureOptionsMonitor
{
    public class CacheEntryOptionsConfigurations
        : IEnumerable<IConfigureOptions<CacheEntryOptions>>
    {
        private readonly IDictionary<string, Lazy<IConfigureOptions<CacheEntryOptions>>> _configurations =
            new Dictionary<string, Lazy<IConfigureOptions<CacheEntryOptions>>>();

        private readonly IDictionary<string, CancellationTokenSource> _cancellationTokenSources =
            new Dictionary<string, CancellationTokenSource>();


        public CacheEntryOptionsConfigurations AddConfiguration(string name, Action<CacheEntryOptions> configure)
        {
            if (_cancellationTokenSources.TryGetValue(name, out var cts))
            {
                _configurations.Remove(name);
                _cancellationTokenSources.Remove(name);
            }
            _configurations.Add(name, new Lazy<IConfigureOptions<CacheEntryOptions>>(
                () => new ConfigureNamedOptions<CacheEntryOptions>(name, configure)));
            _cancellationTokenSources.Add(name, new CancellationTokenSource());
            cts?.Cancel();
            return this;
        }

        public IEnumerable<IOptionsChangeTokenSource<CacheEntryOptions>> Cancellations =>
            _cancellationTokenSources.Select(kv => new CacheEntryOptionsChangeTokenSource(kv.Key, () => GetChangeToken(kv.Key)));

        public IEnumerator<IConfigureOptions<CacheEntryOptions>> GetEnumerator() => _configurations.Values
            .Select(it => it.Value).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IChangeToken GetChangeToken(string name)
        {
            if (_cancellationTokenSources.TryGetValue(name, out var cts))
            {
                return new CancellationChangeToken(cts.Token);
            }
            throw new InvalidOperationException("something goes wrong");
        }
    }
}
