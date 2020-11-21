using System;
using Microsoft.Extensions.Primitives;

namespace CompositeChangeTokenPrimitive
{
    public class SpyChangeToken : IChangeToken
    {
        private readonly  IChangeToken _changeToken;

        public SpyChangeToken(IChangeToken changeToken) => _changeToken = changeToken;

        public bool HasChanged => _changeToken.HasChanged;

        public bool ActiveChangeCallbacks => _changeToken.ActiveChangeCallbacks;

        public IDisposable RegisterChangeCallback(Action<object> callback, object state) =>
            new LogDisposable(_changeToken.RegisterChangeCallback(callback, state));

        private class LogDisposable : IDisposable
        {
            private readonly IDisposable _disposable;

            public LogDisposable(IDisposable disposable) => _disposable = disposable;

            public void Dispose()
            {
                Console.WriteLine("disposed");
                _disposable.Dispose();
            }
        }
    }
}
