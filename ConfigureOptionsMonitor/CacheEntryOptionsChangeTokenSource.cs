using System;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace ConfigureOptionsMonitor
{
    public class CacheEntryOptionsChangeTokenSource : IOptionsChangeTokenSource<CacheEntryOptions>
    {
        public string Name { get; }
        private readonly Func<IChangeToken> _fn;

        public CacheEntryOptionsChangeTokenSource(string name, Func<IChangeToken> fn)
        {
            Name = name;
            _fn = fn;
        }

        public IChangeToken GetChangeToken() => _fn.Invoke();
    }
}
