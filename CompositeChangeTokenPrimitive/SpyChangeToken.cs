using System;
using Microsoft.Extensions.Primitives;

namespace CompositeChangeTokenPrimitive
{
    public class SpyChangeToken : IChangeToken
    {
        private readonly  IChangeToken _origin;

        public SpyChangeToken(IChangeToken origin) => _origin = origin;

        public bool HasChanged => _origin.HasChanged;

        public bool ActiveChangeCallbacks => _origin.ActiveChangeCallbacks;

        public IDisposable RegisterChangeCallback(Action<object> callback, object state) =>
            new LogDisposable(_origin.RegisterChangeCallback(callback, state));

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
